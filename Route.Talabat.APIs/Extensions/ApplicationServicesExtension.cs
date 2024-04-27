using Microsoft.AspNetCore.Mvc;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helper;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Infrastructure;

namespace Route.Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection ApplicationServices(this IServiceCollection services)
        {
            //Basket Repository
             services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            // services.AddScoped<IBasketRepository, BasketRepository>();

            // services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();
            // services.AddScoped<IGenericRepository<ProductBrand>, GenericRepository<ProductBrand>>();
            // services.AddScoped<IGenericRepository<ProductCategory>, GenericRepository<ProductCategory>>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));
            services.AddAutoMapper(typeof(MappingProfiles));


            //instead -> modelstate.isvalid at every api

            //vaildation errors
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                        .SelectMany(p => p.Value.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors,
                    };
                    //cant use badRequest();
                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}
