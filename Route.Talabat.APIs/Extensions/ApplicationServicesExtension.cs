using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Route.Talabat.APIs.Errors;
using Route.Talabat.APIs.Helper;
using Route.Talabat.Core.IRepositories;
using Route.Talabat.Infrastructure;
using System.Text;

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

        public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {

            //builder.Services.AddAuthentication();
            //Called By Default when add above [builder.Services.AddIdentity]
            //default Schema "Application.Identity"



            //if we dont add "Bearer" this configration will be to default
            //"Bearer"

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options =>
            //   {
            //       options.TokenValidationParameters = new TokenValidationParameters()
            //       {
            //           ValidateIssuer = true,
            //           ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            //           ValidateAudience = true,
            //           ValidAudience = builder.Configuration["JWT:ValidAudience"],
            //           ValidateIssuerSigningKey = true,
            //           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:AuthKey"] ?? string.Empty)),
            //           ValidateLifetime=true,
            //           ClockSkew=TimeSpan.Zero, //changes at times in different Server
            //       };
            //   });

             services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters()
                  {
                      ValidateIssuer = true,
                      ValidIssuer = configuration["JWT:ValidIssuer"],
                      ValidateAudience = true,
                      ValidAudience = configuration["JWT:ValidAudience"],
                      ValidateIssuerSigningKey = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:AuthKey"] ?? string.Empty)),
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero, //changes at times in different Server
                  };
              });

            return services;
        }
    }
}
