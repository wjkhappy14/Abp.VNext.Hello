using Volo.Abp.Modularity;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(HelloApplicationModule),
        typeof(HelloDomainTestModule)
        )]
    public class HelloApplicationTestModule : AbpModule
    {

    }
}