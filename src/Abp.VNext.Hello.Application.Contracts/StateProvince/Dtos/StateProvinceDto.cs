using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{

    public class StateProvinceDto : EntityDto<int>
    {
        public StateProvinceDto()
        {
            Cities = new List<CityDto>();
        }

        public string StateProvinceCode { get; set; }
        public string CountryCode { get; set; }
        public string Territory { get; set; }
        public string Name { get; set; }
        public string ChineseName { get; set; }
        public int CountryId { get; set; }

        public ICollection<CityDto> Cities { get; set; }

    }
}
