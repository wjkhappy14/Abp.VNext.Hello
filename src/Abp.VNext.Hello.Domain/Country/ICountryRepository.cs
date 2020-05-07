using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface ICountryRepository : IBasicRepository<Country, string>
    {

        Task<Country> GetByNameAsync(string name);
        IQueryable<Country> Search(string keyword);

        Task<Country> GetCountryByIdAsync(string id);
    }
}
