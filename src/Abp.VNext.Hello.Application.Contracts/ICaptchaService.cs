using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface ICaptchaService : IApplicationService
    {
        Task<byte[]> CreateImageAsync(CaptchaItem captchaItem);

        Task<CaptchaItem> GetItemAsync(string sessionKey);
    }
}
