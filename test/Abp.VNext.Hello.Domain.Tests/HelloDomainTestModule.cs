using Abp.VNext.Hello.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Abp.VNext.Hello
{
    [DependsOn(
        typeof(HelloEntityFrameworkCoreTestModule)
        )]
    public class HelloDomainTestModule : AbpModule
    {

    }
}