using DOS.Estoque.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace DOS.Estoque.Data.Mappings
{
    public class EstoqueSanguineoMapping : IEntityTypeConfiguration<EstoqueSanguineo>
    {
        public void Configure(EntityTypeBuilder<EstoqueSanguineo> builder)
        {
            builder.ToTable("Estoque");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Tipo)
                .IsRequired()
                .HasConversion<string>(); 

            builder.Property(e => e.Unidades)
            .IsRequired();

            builder.Property(e => e.ContadorDoacoes)
                .IsRequired();


            builder.Ignore("_contadorDoacoes");
        }
    }
}




