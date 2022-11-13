
using Concessionaria.Dominio.Core;
using Concessionaria.Dominio.Models;
using Concessionaria.Dominio.Models.Enumerations;
using Concessionaria.Infraestrutura.Mappings;
using Concessionaria.Infraestrutura.Utils;
using Microsoft.EntityFrameworkCore;

namespace Concessionaria.Infraestrutura.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<TipoCarro> TiposCarros { get; set; }
        public DbSet<TipoUsuario> TiposUsuarios { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            modelBuilder.ApplyConfiguration(new EnumerationMapping<TipoCarro>());
            modelBuilder.ApplyConfiguration(new EnumerationMapping<TipoUsuario>());
            
            modelBuilder
                .Entity<TipoCarro>()
                .HasData(Enumeration.GetAll<TipoCarro>());

            modelBuilder
                .Entity<TipoUsuario>()
                .HasData(Enumeration.GetAll<TipoUsuario>());

            modelBuilder.Entity<Usuario>()
                .HasData(new Usuario
                {
                    Id = 1,
                    Nome = "Root Admin",
                    Senha = PasswordHasher.Hash("senha123"),
                    NomeUsuario = "admin",
                    Email = "admin@exemplo.com",
                    IdTipoUsuario = TipoUsuario.Admin.Id
                });
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CriadoEm") != null || entry.Entity.GetType().GetProperty("UpdatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CriadoEm").CurrentValue = DateTime.Now;
                    entry.Property("AtualizadoEm").CurrentValue = null;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CriadoEm").IsModified = false;
                    entry.Property("AtualizadoEm").CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
