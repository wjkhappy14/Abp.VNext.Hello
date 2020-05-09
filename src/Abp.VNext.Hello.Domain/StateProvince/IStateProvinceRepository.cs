using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface IStateProvinceRepository : IBasicRepository<StateProvince, int>
    {

        Task<StateProvince> FindByNameAsync(string name);

        IQueryable<StateProvince> FindByCountryIdAsync(string countryId);

    }
}
