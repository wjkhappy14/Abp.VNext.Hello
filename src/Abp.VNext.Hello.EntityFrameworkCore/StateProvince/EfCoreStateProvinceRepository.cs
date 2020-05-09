using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{

    /// <summary>
    /// Dapper EF 混合应用
    /// https://docs.microsoft.com/zh-cn/archive/msdn-magazine/2016/may/data-points-dapper-entity-framework-and-hybrid-apps
    /// </summary>
    public class EfCoreStateProvinceRepository : EfCoreRepository<HelloDbContext, StateProvince, int>, IStateProvinceRepository
    {
        readonly DapperRepository<HelloDbContext> _dapperRepository;
        public EfCoreStateProvinceRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
            _dapperRepository = new DapperRepository<HelloDbContext>(dbContextProvider);
        }

        public IQueryable<StateProvince> FindByCountryIdAsync(string countryId)
        {
            return DbSet.Where(x => x.CountryId == countryId);
        }

        public  Task<StateProvince> FindByNameAsync(string name)
        {
            return  DbSet.FirstOrDefaultAsync(p => p.StateProvinceName == name);
        }
    }
}
