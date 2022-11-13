
using Concessionaria.Dominio.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concessionaria.Infraestrutura.Mappings
{
    public class RegisterMapping<T> : IEntityTypeConfiguration<T> where T : Register
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasIndex(r => r.Id);
            builder.Property(r => r.CriadoEm).IsRequired();
            builder.Property(r => r.AtualizadoEm).IsRequired(false);
        }
    }
}
