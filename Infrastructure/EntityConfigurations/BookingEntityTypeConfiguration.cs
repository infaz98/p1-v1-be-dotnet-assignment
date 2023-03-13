using Domain.Aggregates.FlightAggregate;
using Domain.Aggregates.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfigurations
{
    public class BookingEntityTypeConfiguration : BaseEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            var navigation = builder.Metadata.FindNavigation(nameof(Order.BookingItems));

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property("PassengerId").IsRequired();

            builder.OwnsOne(o => o.PassengerAddress, a =>
            {
                a.WithOwner();

                a.Property(a => a.ZipCode)
                    .HasMaxLength(18)
                    .IsRequired();

                a.Property(a => a.Street)
                    .HasMaxLength(180)
                    .IsRequired();

                a.Property(a => a.State)
                    .HasMaxLength(60);

                a.Property(a => a.Country)
                    .HasMaxLength(90)
                    .IsRequired();

                a.Property(a => a.City)
                    .HasMaxLength(100)
                    .IsRequired();

            });
        }
    }
}
