using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface IContactRepository : IBasicRepository<Contact, int>
    {

        Task<Contact> GetByNameAsync(string name);
        IQueryable<Contact> Search(string keyword);
    }
}
