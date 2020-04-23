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

        public Task<City> FindByIdAsync(int cityId)
        {

            return DbSet.FirstOrDefaultAsync(w => w.Id == cityId);
        }

        public Task<City> FindByNameAsync(string name)
        {

            return DbSet.FirstOrDefaultAsync(w => w.Name == name);
        }

        public IQueryable<City> SearchAsync(string keywords)
        {
            return DbSet.Where(w => w.Name == keywords);
        }
    }
}
