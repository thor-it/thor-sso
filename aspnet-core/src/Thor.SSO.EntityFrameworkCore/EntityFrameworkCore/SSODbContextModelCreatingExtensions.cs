using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Thor.SSO.EntityFrameworkCore
{
    public static class SSODbContextModelCreatingExtensions
    {
        public static void ConfigureSSO(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(SSOConsts.DbTablePrefix + "YourEntities", SSOConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}