using Abp.VNext.Hello;
using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class EfCoreBlobRepository : EfCoreRepository<HelloDbContext, BlobItem, string>, IBlobRepository
    {

        public EfCoreBlobRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<List<BlobItem>> FindByMerchantIdAsync(Guid? tenantId)
        {
            return await DbSet.Where(w => w.TenantId == tenantId).ToListAsync();
        }

        public async Task<BlobItem> FindByNoAsync(string hash)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Md5 == hash);
        }

    

        public async Task<List<BlobItem>> SearchAsync(Guid? tenantId, DateTime begin, DateTime end)
        {
            return tenantId.HasValue ?
                await DbSet.Where(w => w.TenantId == tenantId.Value && w.CreationTime > begin && w.CreationTime < end).ToListAsync() :
                await DbSet.Where((w) => w.CreationTime > begin && w.CreationTime < end).ToListAsync();
        }

        public Task<List<BlobItem>> SearchAsync(IDictionary tags, DateTime begin, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
