using Abp.VNext.Hello.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VNext.Hello.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class HelloPageModel : AbpPageModel
    {
        protected HelloPageModel()
        {
            LocalizationResourceType = typeof(HelloResource);
        }
    }
}