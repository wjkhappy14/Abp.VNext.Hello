using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class EfCoreCityRepository : EfCoreRepository<HelloDbContext, City, int>, ICityRepository
    {

        public EfCoreCityRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<City> FindByIdAsync(int cityId)
        {
            return await (await GetDbContextAsync()).Set<City>().FirstOrDefaultAsync(w => w.Id == cityId);
        }

        public async Task<City> FindByNameAsync(string name)
        {
            return await(await GetDbContextAsync()).Set<City>().FirstOrDefaultAsync(w => w.Name == name);
        }

        public async Task<IQueryable<City>> SearchAsync(string keywords)
        {
            return  (await GetDbSetAsync()).Where(w => w.Name == keywords);
        }
    }
}
