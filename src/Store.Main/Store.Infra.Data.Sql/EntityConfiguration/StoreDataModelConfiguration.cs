using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Infra.Data.Sql.Model;

namespace Store.Infra.Data.Sql.EntityConfiguration;
public class StoreDataModelConfiguration : IEntityTypeConfiguration<StoreDataModel>
{
    public void Configure(EntityTypeBuilder<StoreDataModel> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).HasMaxLength(50).IsRequired();
        builder.Property(a => a.Phone).HasMaxLength(15).IsRequired();
        builder.Property(a => a.Email).HasMaxLength(80).IsRequired();
    }
}
