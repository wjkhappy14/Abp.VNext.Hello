using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers
{
    [RemoteService(Name = "XLogin")]
    [AllowAnonymous]
    public class XLoginController : AbpController
    {
        IAuthenticationSchemeProvider AuthenticationSchemeProvider { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        UserManager<IdentityUser> UserManager { get; }

        public XLoginController(IAuthenticationSchemeProvider  authenticationSchemeProvider, IAuthorizationHandler authorizationHandler)
        {
            AuthenticationSchemeProvider = authenticationSchemeProvider;
        }

        [HttpPost]
        public async Task<IActionResult> LoginCookies()
        {
            ClaimsIdentity identity = new ClaimsIdentity(new[] { new Claim("ClaimA", "Value") }, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(scheme: null, new ClaimsPrincipal(identity));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginClaimA()
        {
            ClaimsIdentity identity = new ClaimsIdentity(new[] { new Claim("ClaimA", "Value") }, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> LoginClaimAB()
        {
            ClaimsIdentity identity = new ClaimsIdentity(new[] {
                new Claim("ClaimA", "Value"),
                new Claim("ClaimB", "Value")
            }, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            return Ok();
        }

        public IActionResult LoginBearer()
        {

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(new string('x', 128)));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            ClaimsIdentity identity = new ClaimsIdentity(new[] { new Claim("ClaimA", "Value") });
            string Issuer = "issuer.abc.com";
            string Audience = "audience.abc.com";

            JwtSecurityToken token = new JwtSecurityToken(Issuer, Audience, identity.Claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);
            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Content(jwt);
        }
    }

}
