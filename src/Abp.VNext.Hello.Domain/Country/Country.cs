using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace Abp.VNext.Hello
{
    public class Country : AggregateRoot<int>
    {
        public string Name { get; set; }

        public string PostUrl { get; set; }

        public string Comment { get; set; }

        public string Bizs { get; set; }

        public bool Enable { get; set; }
        public virtual Country SetName(string name)
        {
            name = Check.NotNullOrWhiteSpace(name, nameof(name));
            return this;
        }


    }
}
