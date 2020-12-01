using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface IEztvService : IApplicationService
    {

        Task<int> DownloadAsync(int size, int page);

        Task<IEnumerable<EztvItemDto>> GetPagesAsync(int page = 1, int limit = 100);

    }
}
