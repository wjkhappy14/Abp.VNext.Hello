using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public IQueryable<StateProvince> FindByCountryIdAsync(int countryId)
        {
            return DbSet.Where(x => x.CountryId == countryId);
        }

        public  Task<StateProvince> FindByNameAsync(string name)
        {
            return  DbSet.FirstOrDefaultAsync(p => p.StateProvinceName == name);
        }
    }
}
