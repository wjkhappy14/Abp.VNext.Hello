using System;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{
    public class CountryDto : EntityDto<int>
    {
        public string CountryName { get; set; }
        public virtual string ChineseName { get; set; }
        public string FormalName { get; set; }
        public string IsoAlpha3Code { get; set; }
        public int? IsoNumericCode { get; set; }
        public string CountryType { get; set; }
        public string Continent { get; set; }
        public string Region { get; set; }
        public string Subregion { get; set; }

    }
}
