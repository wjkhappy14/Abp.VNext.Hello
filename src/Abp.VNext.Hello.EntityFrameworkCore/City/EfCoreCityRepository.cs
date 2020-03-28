using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<List<City>> FindByMerchantIdAsync(int merchantId)
        {
            return await DbSet.Where(w => w.MerchantId == merchantId).ToListAsync();
        }

        public async Task<City> FindByNoAsync(string serialNo)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.SerialNo == serialNo);
        }

        public async Task<List<City>> SearchAsync(int? merchantId, DateTime begin, DateTime end)
        {
            return merchantId.HasValue ? await DbSet.Where(w => w.MerchantId == merchantId.Value && w.ReqTime > begin && w.ReqTime < end).ToListAsync() : await DbSet.Where((w) => w.ReqTime > begin && w.ReqTime < end).ToListAsync();
        }
    }
}
