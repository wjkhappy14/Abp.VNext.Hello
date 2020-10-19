using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace Abp.VNext.Hello
{
    public class AuthService
    {

        IAuthenticationSchemeProvider Schemes { get; }
        IAuthenticationHandlerProvider Handlers { get; }
        IClaimsTransformation Transform { get; }
        AuthenticationOptions Options { get; }


        public AuthService(IAuthenticationSchemeProvider schemes, IAuthenticationHandlerProvider handlers, IClaimsTransformation transform, IOptions<AuthenticationOptions> options)
        {
            Schemes = schemes;
            Handlers = handlers;
            Transform = transform;
            Options = options.Value;
        }
    }
}
