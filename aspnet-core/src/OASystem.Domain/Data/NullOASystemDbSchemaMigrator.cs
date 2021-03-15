using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace OASystem.Data
{
    /* This is used if database provider does't define
     * IOASystemDbSchemaMigrator implementation.
     */
    public class NullOASystemDbSchemaMigrator : IOASystemDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}