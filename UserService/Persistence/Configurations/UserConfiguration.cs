using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserDal>
{
    public void Configure(EntityTypeBuilder<UserDal> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);

        builder.Property(user => user.NickName).HasMaxLength(20).IsRequired();
        builder.Property(user => user.Country).HasMaxLength(20);
        builder.Property(user => user.AvatarUrl).HasMaxLength(100);

        builder
            .HasOne(user => user.Info)
            .WithOne(info => info.User)
            .HasForeignKey<UserInfoDal>(info => info.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}