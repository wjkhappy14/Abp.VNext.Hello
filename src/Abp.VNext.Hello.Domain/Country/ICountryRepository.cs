using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface ICountryRepository : IBasicRepository<Country, int>
    {

        Task<Country> GetByNameAsync(string name);
        Task<IQueryable<Country>> Search(string keyword);

        Task<Country> GetCountryByIdAsync(int id);
    }
}
