using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebProgrammingTerm.Auth.Core.Models;

namespace WebProgrammingTerm.Auth.Repository.Configurations;

public class RefreshTokenConfiguration:IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.HasKey(t => t.UserId);
        builder.Property(t => t.Token).IsRequired();
    }
}