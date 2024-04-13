
using Microsoft.EntityFrameworkCore;
using Route.Talabat.Infrastructure.Data;

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
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           
            //to swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options =>{
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            #endregion

            var app = builder.Build();


            //used using to close it after finishing and can use try finally
            using var scoped = app.Services.CreateScope();    //create scoped lifetime
            var services = scoped.ServiceProvider;      //has services with scoped lifetime
            var _dbContext = services.GetRequiredService<ApplicationDbContext>();   //ask CLR to Create object from DbContext Explicitly
        

            //may be a problem when update-database and want to view or log error
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        
            try
            {
                await _dbContext.Database.MigrateAsync(); //update-database
                await ApplicationDbContextSeed.SeedAsync(_dbContext);//Data Seeding
               
            }
            catch (Exception ex)
            {
                //  Console.WriteLine(ex.Message);
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex.Message, "An error has been Occured during applaymigrations");
               // logger.LogError(ex.StackTrace.ToString());//more details
            }
        


            //define middleware for the app
            #region Configure Kestrel Middlewares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection(); //if request came as http will redirect to https

            //app.UseAuthorization(); //not need now!


            app.MapControllers();
            //execute Routing at controller itself
            //instead of [ UseRouting & UseEndPoints ] --> at mvc that routing configration for all here not at each controller.

            #endregion

            app.Run();
        }
    }
}
