using Abp.VNext.Hello.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Abp.VNext.Hello.Permissions;

public class HelloPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(HelloPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(HelloPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<HelloResource>(name);
    }
}
