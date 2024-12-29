using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTubeApiCleanArchitecture.Infrastructure.Outbox;

namespace YouTubeApiCleanArchitecture.Infrastructure.Configurations;
internal sealed class OutboxMessagesConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {  
        builder.Property(outboxMessages => outboxMessages.Content)
            .HasColumnType("nvarchar(max)")
            .IsRequired();
    }
}