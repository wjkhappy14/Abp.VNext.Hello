using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface IContactService : IApplicationService
    {
        Task<ContactDto> GetByNameAsync(string name);

        Task<List<ContactDto>> SearchAsync(
     string sorting = null,
     int maxResultCount = int.MaxValue,
     int skipCount = 0,
     string filter = null
     );
    }
}
