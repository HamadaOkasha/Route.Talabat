
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.APIs.Helper;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.Core.Entities.Identity;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;
using Route.Talabat.Infrastructure.Identity;
using StackExchange.Redis;
using System.Net;
using System.Text.Json;

namespace Route.Talabat.APIs
{
    public class Program
    {
        //Entery Point
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            //Register Services to DI Container
            #region Configure Services
            // Add services to the container.

            builder.Services.AddControllers();
            
            builder.Services.AddSwaggerServices();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>{
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
           
            builder.Services.AddDbContext<ApplicationIdentityDbContext>(options=>{
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            //basket
            builder.Services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
                {
                    var connection = builder.Configuration.GetConnectionString("Redis");
                    return ConnectionMultiplexer.Connect(connection);

                });

            //ApplicationServicesExtension.ApplicationServices(builder.Services);
            builder.Services.ApplicationServices();
            
            
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
               // options.Password.RequiredUniqueChars = 2;
               // options.Password.RequireDigit=true;
               // options.Password.RequireLowercase=true;
               // options.Password.RequireUppercase=true;

            }).AddEntityFrameworkStores<ApplicationIdentityDbContext>();
            

            #endregion

            var app = builder.Build();

            //used using to close it after finishing and can use try finally
            using var scoped = app.Services.CreateScope();    //create scoped lifetime
            var services = scoped.ServiceProvider;      //has services with scoped lifetime
            var _dbContext = services.GetRequiredService<ApplicationDbContext>();   //ask CLR to Create object from DbContext Explicitly
            var _identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();   //ask CLR to Create object from IdentityDbContext Explicitly


            //may be a problem when update-database and want to view or log error
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                await _dbContext.Database.MigrateAsync(); //update-database
                await ApplicationDbContextSeed.SeedAsync(_dbContext);//Data Seeding
               
                await _identityDbContext.Database.MigrateAsync(); //update-database

                var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await ApplicationIdentityDataSeeding.SeedUserAsync(_userManager);
            }
            catch (Exception ex)
            {
                //  Console.WriteLine(ex.Message);
                logger.LogError(ex.Message, "An error has been Occured during applaymigrations");
               // logger.LogError(ex.StackTrace.ToString());//more details
            }



            //define middleware for the app
            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
          
            app.UseMiddleware<ExceptionMiddleware>();
            
            //3 speedly middleware
            //app.Use(async (httpContext, _next) =>
            //{
            //    try
            //    {

            //        //if i want take an action with the request 

            //        await _next.Invoke(httpContext);//go To next Middleware

            //        //if i want take an action with the response 
            //    }
            //    catch (Exception ex)
            //    {
            //        logger.LogError(ex.Message);          //development Env
            //        /// log exception in (Database | Files)  //Production Env

            //        //httpContext.Response.StatusCode = 500;
            //        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //        httpContext.Response.ContentType = "application/json";

            //        var response = app.Environment.IsDevelopment() ?
            //            new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
            //            :
            //            new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
            //        //to return to front as camelCase
            //        var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            //        var json = JsonSerializer.Serialize(response);
            //        //   var json = JsonSerializer.Serialize(response, options);
            //        await httpContext.Response.WriteAsync(json);
            //    }

            //});


            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();

               //500
               //app.UseDeveloperExceptionPage();//call internaly from .net
            }


            //  //no api with this name
            //  app.UseStatusCodePagesWithRedirects("/errors/{0}");
            //  //two request -> 302 redirect then 404 not found

            //use this better than above -> one request 404 not found 
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            

            app.UseHttpsRedirection(); //if request came as http will redirect to https

            app.UseStaticFiles();

            //app.UseAuthorization(); //not need now!


            app.MapControllers();
            //execute Routing at controller itself
            //instead of [ UseRouting & UseEndPoints ] --> at mvc that routing configration for all here not at each controller.

            #endregion

            app.Run();
        }
    }
}
