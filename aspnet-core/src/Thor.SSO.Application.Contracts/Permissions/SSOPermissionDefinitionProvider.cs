using Thor.SSO.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Thor.SSO.Permissions
{
    public class SSOPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SSOPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(SSOPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SSOResource>(name);
        }
    }
}
