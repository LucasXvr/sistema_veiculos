using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Configuração para ler do appsettings.json e appsettings.secret.json
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("appsettings.secret.json", optional: true, reloadOnChange: true);

// Altere a configuração da string de conexão do MySQL
var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 29)))
        .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IFileStorageService, FileStorageService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
