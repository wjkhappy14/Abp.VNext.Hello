using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public Task<StateProvince> FindByNameAsync(string name)
        {
            return DbSet.FirstOrDefaultAsync(p => p.StateProvinceName == name);
        }


        private Dictionary<string, List<StateProvince>> W()
        {
            return base.DbContext.StateProvinces
                 .GroupBy(c => c.CountryId)
                 .Where(x => x.Count() > 0)
              .Select(g => new
              {
                  CountryId = g.Key,
                  Items = g.ToList()
              }).OrderByDescending(x => x.Items.Count)
                .ToDictionary(g => g.CountryId, g => g.Items);
        }
        private void c()
        {
            var groupJoin = 
                DbContext.StateProvinces.GroupJoin(
                DbContext.Countries, stateProvince => stateProvince.CountryId,
                country => country.Id,
                (c, p) => new { c.CountryId, Provinces = p });

        }

        void GroupBy(string provinceName)
        {
            var queryable = base.DbContext.StateProvinces
                .Where(x => x.StateProvinceName == provinceName)
                .OrderByDescending(x => x.ChineseName)
                .GroupBy(x => new { x.CountryId, x.StateProvinceName })
                .Select(g => new
                {
                    SumOfCountryPopulation = g.Sum(x => x.Population),
                    CityCount = g.Sum(x => x.Cities.Count),
                    StateProvinces = g.ToList(),
                    g.Key.CountryId,
                    g.Key.StateProvinceName
                });
            var result = queryable;
        }
    }
}
