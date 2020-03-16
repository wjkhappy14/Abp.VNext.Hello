using Volo.Abp.Settings;

namespace Abp.VNext.Hello.Settings
{
    public class HelloSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(HelloSettings.MySetting1));
        }
    }
}
