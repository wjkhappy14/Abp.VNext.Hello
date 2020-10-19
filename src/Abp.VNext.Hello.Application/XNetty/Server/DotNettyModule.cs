using DotNetty.Transport.Channels;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Abp.VNext.Hello.XNetty.Server
{
    public class DotNettyModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            base.ConfigureServices(context);

            Configure<ChannelOption>(options =>
            {

            });
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }
    }
}
