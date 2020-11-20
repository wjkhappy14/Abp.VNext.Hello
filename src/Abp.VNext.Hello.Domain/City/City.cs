using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello
{
    public partial class City : Entity<int>
    {
        public City()
        {
        }
        public virtual int StateProvinceId { get; set; }
        public virtual string Location { get; set; }
        public virtual string Name { get; set; }
        public virtual string StateProvinceCode { get; set; }
        public virtual int CountryId { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Latitude { get; set; }
        public virtual string Longitude { get; set; }
        public virtual string ChineseName { get; set; }
        public int Population { get; set; }

        //https://docs.microsoft.com/zh-cn/sql/t-sql/spatial-geography/spatial-types-geography?view=sql-server-ver15
        // [JsonIgnore]
    }

}
