using Volo.Abp.Settings;

namespace Abp.VNext.Hello.Settings
{
    public class HelloSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(new SettingDefinition(HelloSettings.Port));
        }
    }
}
