using DOS.Usuario.Domain;
using DOS.Usuario.Domain.Enums;
using DOS.Usuario.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace DOS.Usuario.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(x => x.CPF)
                .IsRequired()
                .HasConversion(
                    cpf => cpf.Numero, 
                    entrada => new CPF(entrada))
                .HasColumnType("varchar(11)");

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasConversion(
                    telefone => telefone.Numero,
                    entrada => new Telefone(entrada))
                .HasColumnType("varchar(15)");

            builder.Property(x => x.TipoSanguineo)
                .HasConversion(
                    tipo => tipo.HasValue ? tipo.Value.ToString() : null,
                    entrada => string.IsNullOrEmpty(entrada) ? null :
                    (TipoSanguineo)Enum.Parse(typeof(TipoSanguineo), entrada))
                .HasColumnType("varchar(20)");


        }
    }
}
