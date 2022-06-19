using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Payments.Domain.Entities;
using Payments.Domain.Enums;

namespace Payments.Domain.Configurations;

public class PaymentsConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Status)
            .HasConversion(
                x => x.ToString(),
                x => Enum.Parse<PaymentStatus>(x)
            );
    }
}
