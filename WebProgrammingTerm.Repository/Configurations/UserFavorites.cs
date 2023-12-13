using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Models;

namespace WebProgrammingTerm.Repository.Configurations
{
    public class UserFavoritesConfiguration : IEntityTypeConfiguration<UserFavorites>
    {
        public void Configure(EntityTypeBuilder<UserFavorites> builder)
        {
            builder
                .HasOne(uf => uf.AppUser)
                .WithMany(u => u.Favorites)
                .HasForeignKey(uf => uf.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}