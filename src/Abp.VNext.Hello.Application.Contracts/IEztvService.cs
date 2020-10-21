using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface IEztvService : IApplicationService
    {

        Task<string> DownloadAsync(int size, int page);

    }
}
