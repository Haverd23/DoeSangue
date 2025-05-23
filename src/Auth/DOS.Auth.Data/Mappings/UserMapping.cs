using DOS.Auth.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DOS.Auth.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(u => u.Senha)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(u => u.Role)
                .IsRequired()
                .HasColumnType("varchar(13)");

            builder.ToTable("Users");
        }
    }
}
   