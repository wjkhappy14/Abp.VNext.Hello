using NetTopologySuite.Geometries;
using System;
using System.Text.Json.Serialization;
using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello
{
    public partial class City : Entity<int>
    {
        public City()
        {
        }
        public virtual string Name { get; set; }
        public virtual string ChineseName { get; set; }
        public virtual int StateProvinceId { get; set; }

        [JsonIgnore]
        public Geometry Location { get; set; }


    }

}
