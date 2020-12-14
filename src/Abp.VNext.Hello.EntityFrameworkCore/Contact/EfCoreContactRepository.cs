using Abp.VNext.Hello.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
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


        public async Task<List<Contact>> SearchAsync(
            string sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            string filter = null,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    c =>
                        c.Name.Contains(filter) ||
                        c.Email.Contains(filter) ||
                        c.Title.Contains(filter) ||
                        c.PhoneNumber.Contains(filter)
                )
                .OrderBy(sorting ?? "id")
                .PageBy(skipCount, maxResultCount)
                .ToListAsync(GetCancellationToken(cancellationToken));
        }
    }
}
