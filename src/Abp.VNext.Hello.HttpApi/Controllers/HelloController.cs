using Abp.VNext.Hello.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Abp.VNext.Hello.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class HelloController : AbpControllerBase
{
    protected HelloController()
    {
        LocalizationResource = typeof(HelloResource);
    }
}
