using Microsoft.AspNetCore.Authentication;
using Volo.Abp.AspNetCore.Mvc.Authentication;

namespace Abp.VNext.Hello.Web.Controllers
{
    public class AccountController : ChallengeAccountController
    {
        IAuthenticationSchemeProvider SchemeProvider;
        public AccountController(IAuthenticationSchemeProvider schemeProvider)
        {
            SchemeProvider = schemeProvider;
        }
    }
}