using Volo.Abp.Settings;

namespace Thor.SSO.Settings
{
    public class SSOSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(SSOSettings.MySetting1));
        }
    }
}
