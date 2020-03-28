using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Abp.VNext.Hello
{
    public interface IStateProvinceRepository : IBasicRepository<StateProvince, int>
    {

        Task<StateProvince> FindByNameAsync(string name);

    }
}
