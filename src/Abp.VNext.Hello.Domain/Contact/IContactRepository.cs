using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface IContactRepository : IBasicRepository<Contact, int>
    {

        Task<Contact> GetByNameAsync(string name);
        Task<List<Contact>> SearchAsync(
        string sorting = null,
        int maxResultCount = int.MaxValue,
        int skipCount = 0,
        string filter = null,
        CancellationToken cancellationToken = default);
    }
}
