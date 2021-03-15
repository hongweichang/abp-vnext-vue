using Volo.Abp.Modularity;

namespace OASystem
{
    [DependsOn(
        typeof(OASystemApplicationModule),
        typeof(OASystemDomainTestModule)
        )]
    public class OASystemApplicationTestModule : AbpModule
    {

    }
}