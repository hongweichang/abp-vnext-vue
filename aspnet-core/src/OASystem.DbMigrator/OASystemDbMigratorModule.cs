using OASystem.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace OASystem.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(OASystemEntityFrameworkCoreDbMigrationsModule),
        typeof(OASystemApplicationContractsModule)
        )]
    public class OASystemDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
