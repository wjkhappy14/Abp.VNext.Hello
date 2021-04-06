using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Hello.Authentication.MiniProgram.WeChat
{
    public class WeChatMiniProgramPostConfigureOptions : IPostConfigureOptions<WeChatMiniProgramOptions>
    {
        private readonly IDataProtectionProvider _dp;

        public WeChatMiniProgramPostConfigureOptions(IDataProtectionProvider dataProtection)
        {
            _dp = dataProtection;
        }

        public void PostConfigure(string name, WeChatMiniProgramOptions options)
        {
            options.DataProtectionProvider = options.DataProtectionProvider ?? _dp;

            if (options.Backchannel == null)
            {
                options.Backchannel = new HttpClient(options.BackchannelHttpHandler ?? new HttpClientHandler());
                options.Backchannel.Timeout = options.BackchannelTimeout;
                options.Backchannel.MaxResponseContentBufferSize = 1024 * 1024 * 10; // 10 MB
                options.Backchannel.DefaultRequestHeaders.Accept.ParseAdd("*/*");
                options.Backchannel.DefaultRequestHeaders.UserAgent.ParseAdd("Microsoft ASP.NET Core Wechat MiniProgram handler");
                options.Backchannel.DefaultRequestHeaders.ExpectContinue = false;
            }
        }
    }
}
