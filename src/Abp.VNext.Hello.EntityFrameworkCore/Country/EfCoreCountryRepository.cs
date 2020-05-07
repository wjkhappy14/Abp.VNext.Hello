using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class EfCoreCountryRepository : EfCoreRepository<HelloDbContext, Country, string>, ICountryRepository
    {

        public EfCoreCountryRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public IQueryable<Country> Search(string keyword)
        {
            return DbSet.Where(w => w.CountryName == keyword);
        }

        public async Task<Country> GetByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.CountryName == name);
        }
        public Task<Country> GetCountryByIdAsync(string id)
        {
            Task<Country> country = DbContext.Countries
                               .AsNoTracking()
                               .Include(item => item.StateProvinces)
                               .FirstOrDefaultAsync(p => p.Id == id);
            return country;
        }
    }
}
