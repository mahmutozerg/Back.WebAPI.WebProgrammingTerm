using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharedLibrary.Models;

namespace WebProgrammingTerm.Repository.Configurations
{
    public class CompanyUserConfiguration : IEntityTypeConfiguration<CompanyUser>
    {
        public void Configure(EntityTypeBuilder<CompanyUser> builder)
        {

        }
    }
}