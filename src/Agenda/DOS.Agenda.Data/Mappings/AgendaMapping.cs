using DOS.Agenda.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DOS.Agenda.Data.Mappings
{
    public class AgendaMapping : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DataHora)
                .IsRequired()
                .HasColumnType("datetime");


            builder.Property(x => x.VagasTotais)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(x => x.VagasOcupadas)
                .IsRequired()
                .HasColumnType("int");

            builder.ToTable("Horarios");


        }
    }
}
