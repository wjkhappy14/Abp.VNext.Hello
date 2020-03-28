using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class EfCoreStateProvinceRepository : EfCoreRepository<HelloDbContext, StateProvince, int>, IStateProvinceRepository
    {

        public EfCoreStateProvinceRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public async Task<StateProvince> FindByNameAsync(string name)
        {
            return await DbSet.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
