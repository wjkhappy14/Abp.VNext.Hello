using Abp.VNext.Hello.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.VNext.Hello
{

    /// <summary>
    /// 定义权限
    /// </summary>
    public class HelloPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            PermissionGroupDefinition moduleGroup = context.AddGroup(HelloPermissions.GroupName, L("Hello"));

            PermissionDefinition city = moduleGroup.AddPermission(HelloPermissions.City.Default, L("Hello:City"));
            city.AddChild(HelloPermissions.City.View, L("Hello:City:View"));
            city.AddChild(HelloPermissions.City.Create, L("Hello:City:Create"));
            city.AddChild(HelloPermissions.City.Update, L("Hello:City:Update"));
            city.AddChild(HelloPermissions.City.Delete, L("Hello:City:Delete"));
            city.AddChild(HelloPermissions.City.Search, L("Hello:City:Search"));

            PermissionDefinition country = moduleGroup.AddPermission(HelloPermissions.Country.Default, L("Hello:Country"));
            country.AddChild(HelloPermissions.Country.View, L("Hello:Country:View"));
            country.AddChild(HelloPermissions.Country.Create, L("Hello:Country:Create"));
            country.AddChild(HelloPermissions.Country.Update, L("Hello:Country:Update"));
            country.AddChild(HelloPermissions.Country.Delete, L("Hello:Country:Delete"));
            country.AddChild(HelloPermissions.Country.Search, L("Hello:Country:Search"));


        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HelloResource>(name);
        }
    }
}
