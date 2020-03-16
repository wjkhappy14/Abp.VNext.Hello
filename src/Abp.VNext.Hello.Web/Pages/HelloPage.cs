using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Abp.VNext.Hello.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Abp.VNext.Hello.Web.Pages
{
    /* Inherit your UI Pages from this class. To do that, add this line to your Pages (.cshtml files under the Page folder):
     * @inherits Abp.VNext.Hello.Web.Pages.HelloPage
     */
    public abstract class HelloPage : AbpPage
    {
        [RazorInject]
        public IHtmlLocalizer<HelloResource> L { get; set; }
    }
}
