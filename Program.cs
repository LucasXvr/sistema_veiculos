using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();


builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("appsettings.secret.json", optional: true, reloadOnChange: true);

// Altere a configuração da string de conexão do MySQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

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
    app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
    app.UseHsts();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
