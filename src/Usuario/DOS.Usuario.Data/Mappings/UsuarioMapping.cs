using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UsuarioEntity = DOS.Usuario.Domain.Usuario;
using CPFEntity = DOS.Usuario.Domain.ValueObjects.CPF;
using TelefoneEntity = DOS.Usuario.Domain.ValueObjects.Telefone;
using TipoSanguineoEnum = DOS.Usuario.Domain.Enums.TipoSanguineo;
namespace DOS.Usuario.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<UsuarioEntity>
    {
        public void Configure(EntityTypeBuilder<UsuarioEntity> builder)
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
                    entrada => new CPFEntity(entrada))
                .HasColumnType("varchar(11)");

            builder.Property(x => x.Telefone)
                .IsRequired()
                .HasConversion(
                    telefone => telefone.Numero,
                    entrada => new TelefoneEntity(entrada))
                .HasColumnType("varchar(15)");

            builder.Property(x => x.TipoSanguineo)
                .HasConversion(
                    tipo => tipo.HasValue ? tipo.Value.ToString() : null,
                    entrada => string.IsNullOrEmpty(entrada) ? null :
                    (TipoSanguineoEnum)Enum.Parse(typeof(TipoSanguineoEnum), entrada))
                .HasColumnType("varchar(20)");


        }
    }
}
