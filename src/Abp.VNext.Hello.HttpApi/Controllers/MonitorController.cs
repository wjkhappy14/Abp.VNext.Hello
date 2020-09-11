using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class MonitorController : AbpController
    {
        IEnumerable<IActionDescriptorProvider> ActionDescriptorProviders { get; }
        public MonitorController(IEnumerable<IActionDescriptorProvider> providers)
        {
            ActionDescriptorProviders = providers;
        }

        [Authorize(AuthenticationSchemes = "My-Cookie")]
        [HttpGet]
        public IActionResult ActionDescriptorProvider()
        {
            return Json(new
            {
                ActionDescriptorProviders
            });
        }

        [AllowAnonymous]
        public IActionResult AllowAnonymous() => Ok();

        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn()
        {
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity("My-Cookie"));

            ClaimsIdentity claimsIdentity = new ClaimsIdentity() { };
            claimsPrincipal.AddIdentity(claimsIdentity);

            await HttpContext.SignInAsync("My-Cookie", claimsPrincipal);
            return Content("My-Cookie");
        }
    }
}
