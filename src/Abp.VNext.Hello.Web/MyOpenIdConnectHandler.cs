using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Abp.VNext.Hello
{

    [Dependency(ReplaceServices = true)]
    public class MyOpenIdConnectHandler : OpenIdConnectHandler
    {

        public MyOpenIdConnectHandler(IOptionsMonitor<OpenIdConnectOptions> options, ILoggerFactory logger, HtmlEncoder htmlEncoder, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, htmlEncoder, encoder, clock)
        {


        }

        protected override bool ValidateCorrelationId(AuthenticationProperties properties)
        {
            return base.ValidateCorrelationId(properties);
        }

        protected override Task<HandleRequestResult> HandleRemoteAuthenticateAsync()
        {
            return base.HandleRemoteAuthenticateAsync();
        }
    }
}
