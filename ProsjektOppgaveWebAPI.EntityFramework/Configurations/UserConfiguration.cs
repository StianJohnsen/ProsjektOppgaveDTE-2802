using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProsjektOppgaveWebAPI.Database.Entities;

namespace ProsjektOppgaveWebAPI.EntityFramework.Configurations;

public class UserConfiguration: IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Username).IsRequired();
        builder.Property(user => user.HashedPassword).IsRequired();
        builder.HasData(new UserEntity()
        {
            Id = 1,
            Username = "admin",
            HashedPassword = "admin"

        });
    }
}