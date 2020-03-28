using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class EfCoreCountryRepository : EfCoreRepository<HelloDbContext, Country, int>, ICountryRepository
    {

        public EfCoreCountryRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Country>> FindByMerchantIdAsync(int merchantId)
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Country> FindByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Name == name);
        }

        public Task<Country> GetByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
