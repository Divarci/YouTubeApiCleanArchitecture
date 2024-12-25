using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems;
using YouTubeApiCleanArchitecture.Domain.Entities.InvoiceItems.ValueObjects;
using YouTubeApiCleanArchitecture.Domain.Entities.Shared;

namespace YouTubeApiCleanArchitecture.Infrastructure.Configurations;
public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.Property(item => item.SellPrice)
            .HasConversion(
                sellPrice => sellPrice.Value,
                value => new Money(value))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(item => item.TotalPrice)
            .HasConversion(
                totalPrice => totalPrice.Value,
                value => new Money(value))
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(item => item.Quantity)
            .HasConversion(
                quantity => quantity.Value,
                value => new Quantity(value))
            .IsRequired();

        builder.Property(x => x.RowVersion)
           .IsRowVersion();
    }
}
