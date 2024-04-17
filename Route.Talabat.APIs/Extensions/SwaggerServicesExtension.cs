using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helper;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Infrastructure;

namespace Route.Talabat.APIs.Extensions
{
    public static class SwaggerServicesExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //to swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
        public static WebApplication UseSwaggerMiddlewares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
