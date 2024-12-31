using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YouTubeApiCleanArchitecture.Domain.Entities.Identity.Users;

namespace YouTubeApiCleanArchitecture.Infrastructure.Configurations;
public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(x => x.Fullname)
             .IsRequired()
             .HasMaxLength(25);
    }
}
