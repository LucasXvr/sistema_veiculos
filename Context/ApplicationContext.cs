using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    public DbSet<Cliente> Cliente { get; set; }
    public DbSet<Veiculo> Veiculo { get; set; }
    public DbSet<Foto> Fotos { get; set; }

    private readonly IConfiguration _configuration;

    public ApplicationContext(DbContextOptions<ApplicationContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("Default");

                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)))
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();

                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)),
                    mySqlOptions => mySqlOptions.EnableRetryOnFailure());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao configurar o DbContext: {ex.Message}");
                throw;
            }
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Foto>()
            .HasOne(f => f.Veiculo)
            .WithMany(v => v.Fotos)
            .HasForeignKey(f => f.VeiculoId);

        modelBuilder.Entity<Veiculo>()
            .HasOne(v => v.Cliente)
            .WithMany(c => c.Veiculos)
            .HasForeignKey(v => v.ClienteId);
    }
}
