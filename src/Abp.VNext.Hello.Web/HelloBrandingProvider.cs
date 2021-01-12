using Volo.Abp.DependencyInjection;

namespace Abp.VNext.Hello.Web
{
    [Dependency(ReplaceServices = true)]
    public class HelloBrandingProvider// : DefaultBrandingProvider
    {
        public  string AppName => "Hello";
    }
}
