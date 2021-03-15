using OASystem.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace OASystem
{
    [DependsOn(
        typeof(OASystemEntityFrameworkCoreTestModule)
        )]
    public class OASystemDomainTestModule : AbpModule
    {

    }
}