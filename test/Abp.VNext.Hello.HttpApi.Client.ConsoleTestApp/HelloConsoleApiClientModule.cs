using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Abp.VNext.Hello.HttpApi.Client.ConsoleTestApp
{
    [DependsOn(
        typeof(HelloHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class HelloConsoleApiClientModule : AbpModule
    {
        
    }
}
