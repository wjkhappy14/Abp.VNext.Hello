using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface ISchedulerRepository : IBasicRepository<Scheduler, int>
    {

        Task<Scheduler> GetByNameAsync(string name);
        IQueryable<Scheduler> Search(string keyword);
    }
}
