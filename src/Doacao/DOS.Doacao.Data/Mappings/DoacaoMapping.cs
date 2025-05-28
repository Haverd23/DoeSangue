using DOS.Doacao.Domain;
using DOS.Doacao.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DOS.Doacao.Data.Mappings
{
    public class DoacaoMapping : IEntityTypeConfiguration<DoacaoRegistro>
    {
        public void Configure(EntityTypeBuilder<DoacaoRegistro> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.AgendaId)
                .IsRequired();

            builder.Property(x => x.UsuarioId)
                .IsRequired();

            builder.Property(x => x.DataHoraAgendada)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(x => x.Status)
                .IsRequired()
                .HasConversion(
                    status => status.ToString(),
                    entrada => (StatusDoacao)Enum.Parse(typeof(StatusDoacao), entrada))
                .HasColumnType("varchar(20)");

            builder.Property(x => x.TipoSanguineo)
                .IsRequired(false)
                .HasColumnType("varchar(20)");

        }
    }
}
