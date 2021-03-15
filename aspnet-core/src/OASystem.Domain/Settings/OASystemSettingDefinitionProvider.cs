using Volo.Abp.Settings;

namespace OASystem.Settings
{
    public class OASystemSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(OASystemSettings.MySetting1));
        }
    }
}
