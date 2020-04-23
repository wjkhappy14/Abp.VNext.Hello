using Abp.VNext.Hello.Dtos;
using AutoMapper;

namespace Abp.VNext.Hello
{
    public class HelloApplicationAutoMapperProfile : Profile
    {
        public HelloApplicationAutoMapperProfile()
        {
            CreateMap<Country, CountryDto>();
            CreateMap<StateProvince, StateProvinceDto>();
            CreateMap<City, CityDto>();//.Ignore(x=>x.);
        }
    }
}
