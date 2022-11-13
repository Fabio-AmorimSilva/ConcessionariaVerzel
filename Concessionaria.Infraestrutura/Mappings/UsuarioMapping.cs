
using Concessionaria.Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Concessionaria.Infraestrutura.Mappings
{
    public class UsuarioMapping : RegisterMapping<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            base.Configure(builder);

            builder.Property(u => u.Nome)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.NomeUsuario)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(u => u.Email)
                .HasMaxLength(80)
                .IsRequired();

            builder.Property(u => u.Senha)
                .IsRequired();

            builder.HasOne(u => u.TipoUsuario)
                .WithMany()
                .HasForeignKey(u => u.IdTipoUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(u => u.CriadoEm)
                .HasDefaultValueSql("getdate()");
        }
    }
}
