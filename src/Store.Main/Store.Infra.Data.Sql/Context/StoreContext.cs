using Microsoft.EntityFrameworkCore;
using Store.Infra.Data.Sql.Model;
using Store.Infra.Data.Sql.EntityConfiguration;

namespace Store.Infra.Data.Sql.Context;
public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
    {
    }

    public DbSet<StoreDataModel> Stores { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StoreDataModelConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}

