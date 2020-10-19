using System;
using Volo.Abp.Application.Dtos;

namespace Abp.VNext.Hello.Dtos
{
    public class CountryDto : EntityDto<int>
    {
        public virtual string Name { get; set; }
        public virtual string PhoneCode { get; set; }
        public virtual string Capital { get; set; }
        public virtual string Currency { get; set; }
        public virtual string FormalName { get; set; }
        public virtual string IsoAlpha3Code { get; set; }
        public virtual string IsoNumericCode { get; set; }
        public virtual string CountryType { get; set; }
        public virtual string Continent { get; set; }
        public virtual string Region { get; set; }
        public virtual string Subregion { get; set; }
        public virtual string Border { get; set; }
        public virtual string ChineseName { get; set; }
        public virtual string IsoAlpha2Code { get; set; }

    }
}
