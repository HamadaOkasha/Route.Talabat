
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Extensions;
using Route.Talabat.APIs.Helper;
using Route.Talabat.APIs.Middlewares;
using Route.Talabat.Core.Entities.Product;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Infrastructure;
using Route.Talabat.Infrastructure.Data;
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
            

            //ApplicationServicesExtension.ApplicationServices(builder.Services);
            builder.Services.ApplicationServices();
            

            #endregion

            var app = builder.Build();

            //used using to close it after finishing and can use try finally
            using var scoped = app.Services.CreateScope();    //create scoped lifetime
            var services = scoped.ServiceProvider;      //has services with scoped lifetime
            var _dbContext = services.GetRequiredService<ApplicationDbContext>();   //ask CLR to Create object from DbContext Explicitly
        

            //may be a problem when update-database and want to view or log error
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            var logger = loggerFactory.CreateLogger<Program>();
            try
            {
                await _dbContext.Database.MigrateAsync(); //update-database
                await ApplicationDbContextSeed.SeedAsync(_dbContext);//Data Seeding
               
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
