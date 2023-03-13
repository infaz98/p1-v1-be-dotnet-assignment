using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations;

public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
{
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        base.Configure(builder);
        var navigation = builder.Metadata.FindNavigation(nameof(Order.Passengers));
        
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        
        builder.Property("OrderDate").IsRequired();
        builder.Property("NoOfSeats").IsRequired();
        builder.HasOne<Passenger>()
            .WithMany()
            .IsRequired()
            .HasForeignKey("PassengerId");
    }
}