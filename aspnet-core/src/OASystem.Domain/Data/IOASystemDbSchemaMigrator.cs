using System.Threading.Tasks;

namespace OASystem.Data
{
    public interface IOASystemDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
