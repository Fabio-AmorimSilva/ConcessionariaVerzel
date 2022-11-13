using Concessionaria.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Concessionaria.Infraestrutura.Mappings
{
    public class CarroMapping : RegisterMapping<Carro>
    {
        public override void Configure(EntityTypeBuilder<Carro> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Nome)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Modelo)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(c => c.Marca)
                .HasMaxLength(50);

            builder.Property(c => c.Valor)
                .IsRequired();

            builder.HasOne(c => c.TipoCarro)
                .WithMany()
                .HasForeignKey(c => c.IdTipoCarro)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.CriadoEm)
                .HasDefaultValueSql("getdate()");

        }
    }
}
