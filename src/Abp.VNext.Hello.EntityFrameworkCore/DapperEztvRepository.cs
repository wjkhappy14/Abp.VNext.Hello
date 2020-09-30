using Abp.VNext.Hello.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.Dapper;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class DapperEztvRepository : DapperRepository<HelloDbContext>, ITransientDependency, IDapperEztvRepository
    {
        public DapperEztvRepository(IDbContextProvider<HelloDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
