using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello
{

    /// <summary>
    /// https://docs.abp.io/zh-Hans/abp/latest/Entities
    /// </summary>
    public partial class Country : Entity<string>
    {
        public Country()
        {

        }

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

        public virtual ICollection<StateProvince> StateProvinces { get; protected set; } = new HashSet<StateProvince>();
    }
}
