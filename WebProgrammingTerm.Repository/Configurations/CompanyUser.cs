using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebProgrammingTerm.Core.Models;

namespace WebProgrammingTerm.Repository.Configurations
{
    public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {
            builder.HasKey(cu => new { cu.CompanyId, cu.UserId });

            builder
                .HasOne(cu => cu.Company)
                .WithMany()
                .HasForeignKey(cu => cu.CompanyId);

            builder
                .HasOne(cu => cu.User)
                .WithMany()
                .HasForeignKey(cu => cu.UserId);
        }
    }
}