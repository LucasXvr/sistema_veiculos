using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

public class ApplicationContext : DbContext
{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Veiculo> Veiculo { get; set; }
    public DbSet<Foto> Fotos { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Foto>()
            .HasOne(f => f.Veiculo)
            .WithMany(v => v.Fotos)
            .HasForeignKey(f => f.VeiculoId);
    }
}
