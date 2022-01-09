using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Abp.VNext.Hello.Web
{
    [Dependency(ReplaceServices = true)]
    public class HelloBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "Hello";
    }
}
