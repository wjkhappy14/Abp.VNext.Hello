using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{

    public class CityDto : EntityDto<int>
    {
        public string StateProvinceId { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
        public string StateProvinceCode { get; set; }
        public string CountryId { get; set; }
        public string CountryCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ChineseName { get; set; }

    }
}
