using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property<int>("CustomerId").IsRequired();
           
            builder.HasOne<FlightRate>()
                .WithMany()
                .HasForeignKey(o => o.FlightRateId);
        }
    }
}
