using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserInfoConfiguration : IEntityTypeConfiguration<UserInfoDal>
{
    public void Configure(EntityTypeBuilder<UserInfoDal> builder)
    {
        builder.ToTable("UsersInfo");
        builder.HasKey(info => info.Id);

        builder.Property(info => info.FirstName).HasMaxLength(15);
        builder.Property(info => info.LastName).HasMaxLength(20);
        builder.Property(info => info.About).HasMaxLength(75);
    }
}