using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.BrandDTOs;

namespace Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //             dto -> model
            CreateMap<AddBrandDTO, Brand>().ReverseMap();
            //           model -> dto
            CreateMap<Brand, GetBrandDTO>().ReverseMap();

            CreateMap<UpdateBrandDTO, Brand>();
        }
    }
}
