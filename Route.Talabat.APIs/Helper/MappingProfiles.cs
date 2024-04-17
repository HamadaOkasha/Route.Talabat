using AutoMapper;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities.Product;

namespace Route.Talabat.APIs.Helper
{
    public class MappingProfiles:Profile
    {

        //// -> here no parameterless ctor defined
        // private readonly IConfiguration _config;

        // public MappingProfiles(IConfiguration config)
        // {
        //     _config = config;

        //     CreateMap<Product, ProductToReturnDto>()
        //         .ForMember(p => p.Brand, O => O.MapFrom(S => S.Brand.Name))
        //         .ForMember(p => p.Category, O => O.MapFrom(S => S.Category.Name))
        //         .ForMember(p => p.PictureUrl, O => O.MapFrom(S => $"{_config["ApiBaseUrl"]}/{S.PictureUrl}")) //error
        //         //.reverseMap() no need here
        //         ;
        // }



        public MappingProfiles()
        {


            CreateMap<Product, ProductToReturnDto>()
                .ForMember(p => p.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(p => p.Category, O => O.MapFrom(S => S.Category.Name))
                //.ForMember(p => p.PictureUrl, O => O.MapFrom(S => $"{_config["ApiBaseUrl"]}/{S.PictureUrl}"))
                .ForMember(p => p.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>())
                //.reverseMap() no need here
                ;
        }

    }
}
