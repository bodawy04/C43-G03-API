using Domain.Models.OrderModels;

namespace Persistence.Data.Configurations;

internal class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.Property(d=>d.SubTotal).HasColumnType("decimal(8,2)");
        builder.HasMany(o => o.Items).WithOne();
        builder.OwnsOne(x => x.Address, x => x.WithOwner());
    }
}
