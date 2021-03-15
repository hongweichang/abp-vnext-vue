using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace OASystem.EntityFrameworkCore
{
    public static class OASystemDbContextModelCreatingExtensions
    {
        public static void ConfigureOASystem(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(OASystemConsts.DbTablePrefix + "YourEntities", OASystemConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}