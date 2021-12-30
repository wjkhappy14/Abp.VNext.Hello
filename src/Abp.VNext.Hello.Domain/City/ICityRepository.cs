using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface ICityRepository : IBasicRepository<City, int>
    {
        Task<City> FindByIdAsync(int cityId);

        Task<City> FindByNameAsync(string name);

        Task<IQueryable<City>> SearchAsync(string keywords);
    }
}
