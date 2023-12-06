using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Repository.Configurations
{
    public class UserCommentsConfiguration : IEntityTypeConfiguration<UserComments>
    {
        public void Configure(EntityTypeBuilder<UserComments> builder)
        {
            builder
                .HasOne(uc => uc.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}