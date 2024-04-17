using AutoMapper;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities.Product;

namespace Route.Talabat.APIs.Helper
{
    //here can resolve any thing like image calculation ....etc
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public ProductPictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_config["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            else
                return string.Empty;
        }
    }
}
