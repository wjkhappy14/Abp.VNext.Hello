using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class ServiceHub : ApplicationService
    {
       // public ICaptchaService CaptchaService => LazyGetRequiredService(ref captchaService);

        public ICaptchaService captchaService;

        public ServiceHub()
        {
        }
    }
}
