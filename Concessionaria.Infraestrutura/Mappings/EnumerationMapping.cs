
using Concessionaria.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concessionaria.Infraestrutura.Mappings
{
    public class EnumerationMapping<T> : IEntityTypeConfiguration<T> where T : Enumeration
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .ValueGeneratedNever();

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
