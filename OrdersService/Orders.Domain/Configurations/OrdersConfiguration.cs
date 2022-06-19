using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orders.Domain.Entities;
using Orders.Domain.Enums;

namespace Orders.Domain.Configurations;

public class OrdersConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Status)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<OrderStatus>(x)
            );

        builder.HasOne(x => x.Product).WithMany(x => x.Orders);
    }
}
