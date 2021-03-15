using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace OASystem
{
    [Dependency(ReplaceServices = true)]
    public class OASystemBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "OASystem";
    }
}
