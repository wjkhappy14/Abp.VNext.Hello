using Abp.VNext.Hello.EntityFrameworkCore;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Abp.VNext.Hello
{
    public class EfCoreContactRepository : EfCoreRepository<HelloDbContext, Contact, int>, IContactRepository
    {
        public EfCoreContactRepository(IDbContextProvider<HelloDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<Contact> GetByNameAsync(string name) => FindAsync(x => x.Name == name);

        public IQueryable<Contact> Search(string keyword) => base.DbContext.Contacts.Where(x => x.Name == keyword);
    }
}
