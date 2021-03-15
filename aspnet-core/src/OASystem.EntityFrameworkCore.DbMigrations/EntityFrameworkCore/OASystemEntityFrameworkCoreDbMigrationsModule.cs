using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace OASystem.EntityFrameworkCore
{
    [DependsOn(
        typeof(OASystemEntityFrameworkCoreModule)
        )]
    public class OASystemEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<OASystemMigrationsDbContext>();
        }
    }
}
