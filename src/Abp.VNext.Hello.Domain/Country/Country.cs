using NetTopologySuite.Geometries;
using Newtonsoft.Json;
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

        public string CountryName { get; set; }
        public virtual string ChineseName { get; set; }
        public string FormalName { get; set; }
        public virtual string IsoAlpha3Code { get; set; }
        public virtual int? IsoNumericCode { get; set; }
        public virtual string CountryType { get; set; }
        public virtual string Continent { get; set; }
        public virtual string Region { get; set; }
        public virtual string Subregion { get; set; }

        [JsonIgnore()]
        public Geometry Border { get; set; }
        public virtual ICollection<StateProvince> StateProvinces { get; protected set; } = new HashSet<StateProvince>();
    }
}
