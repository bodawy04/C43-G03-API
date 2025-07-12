using Domain.Models.OrderModels;

namespace Persistence.Data.Configurations;

internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable("DeliveryMethods");
        builder.Property(d=>d.Price).HasColumnType("decimal(8,2)");
    }
}
