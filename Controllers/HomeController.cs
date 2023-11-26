using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaVeiculos.Models;

namespace SistemaVeiculos.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationContext _context;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<HomeController> _logger;


    public HomeController(ApplicationContext context, IFileStorageService fileStorageService, ILogger<HomeController> logger)
    {
        _logger = logger;
        _context = context;
        _fileStorageService = fileStorageService;
    }

    public IActionResult Index()
    {
        var veiculos = _context.Veiculo.Include(v => v.Fotos)
        .OrderByDescending(v => v.DtCadastro)
        .ToList();
        return View(veiculos);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
