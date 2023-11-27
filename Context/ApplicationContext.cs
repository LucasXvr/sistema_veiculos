using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Veiculo> Veiculo { get; set; }
    public DbSet<Foto> Fotos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Substitua com a sua string de conexão.
            var connectionString = "MySqlConnection";

            // Use 'MySqlServerVersion' para MySQL.
            var serverVersion = ServerVersion.AutoDetect(connectionString);

            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)))
                .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Foto>()
            .HasOne(f => f.Veiculo)
            .WithMany(v => v.Fotos)
            .HasForeignKey(f => f.VeiculoId);
    }
}
