using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class CaptchaService : ApplicationService
    {
        protected ILogger Logger { get; }

        public CaptchaOption Options { get; private set; }

        protected IOptionsMonitor<CaptchaOption> OptionsMonitor { get; }

        public CaptchaService(IOptionsMonitor<CaptchaOption> options, ILoggerFactory logger)
        {
            Logger = logger.CreateLogger(GetType().FullName);
            OptionsMonitor = options;
            Options = OptionsMonitor.Get("simple");
        }
    }
}
