using AutoMapper;
using Route.Talabat.APIs.DTOs;
using Route.Talabat.Core.Entities.Product;

namespace Route.Talabat.APIs.Helper
{
    public class MappingProfiles:Profile
    {

        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(p => p.Brand, O => O.MapFrom(S => S.Brand.Name))
                .ForMember(p => p.Category, O => O.MapFrom(S => S.Category.Name))
                //.reverseMap() no need here
                ;
        }

    }
}
