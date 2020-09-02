using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public class CaptchaService : ApplicationService, ICaptchaService
    {
        protected new ILogger Logger { get; }

        protected IMemoryCache Cache { get; }

        public CaptchaOption Options { get; }

        protected IOptionsMonitor<CaptchaOption> OptionsMonitor { get; }

        public CaptchaService(IOptionsMonitor<CaptchaOption> options, IMemoryCache cache, ILoggerFactory logger)
        {
            Logger = logger.CreateLogger(GetType().FullName);
            OptionsMonitor = options;
            Options = OptionsMonitor.Get("simple");
            Cache = cache;
        }

        public Task<Guid> Code()
        {
            Guid guid = GuidGenerator.Create();
            Cache.CreateEntry(guid);
            return Task.FromResult(guid);
        }
    }
}
