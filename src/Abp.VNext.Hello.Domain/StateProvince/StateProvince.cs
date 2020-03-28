using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello
{
    public class StateProvince : AggregateRoot<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Collection<City> Routers { get; set; }
    }
}
