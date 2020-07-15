using Abp.VNext.Hello;
using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Volo.Blogging.Payment
{
    public class EfCorePictureRepository : EfCoreRepository<HelloDbContext, Picture, int>, IPictureRepository
    {

        public EfCorePictureRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<Picture>> FindByMerchantIdAsync(Guid? tenantId)
        {
            return await DbSet.Where(w => w.TenantId == tenantId).ToListAsync();
        }

        public async Task<Picture> FindByNoAsync(string serialNo)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.SerialNo == serialNo);
        }

        public async Task<List<Picture>> SearchAsync(Guid? tenantId, DateTime begin, DateTime end)
        {
            return tenantId.HasValue ?
                await DbSet.Where(w => w.TenantId == tenantId.Value && w.ReqTime > begin && w.ReqTime < end).ToListAsync() :
                await DbSet.Where((w) => w.ReqTime > begin && w.ReqTime < end).ToListAsync();
        }
    }
}
