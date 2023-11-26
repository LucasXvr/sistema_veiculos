using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class VeiculoController : Controller
{
    private readonly ApplicationContext _context;
    private readonly IWebHostEnvironment _hostingEnvironment;
    private readonly IFileStorageService _fileStorageService;
    private readonly ILogger<VeiculoController> _logger;

    public VeiculoController(ApplicationContext context, IWebHostEnvironment hostingEnvironment, IFileStorageService fileStorageService, ILogger<VeiculoController> logger)
    {
        _context = context;
        _hostingEnvironment = hostingEnvironment;
        _fileStorageService = fileStorageService;
        _logger = logger;
    }

    public IActionResult Index(string searchString)
    {
        // Se houver uma string de busca, filtre os veículos com base nela
        var veiculos = string.IsNullOrEmpty(searchString)
            ? _context.Veiculo.Include(v => v.Fotos).ToList()
            : _context.Veiculo.Include(v => v.Fotos).Where(v => v.MarcaModeloVeiculo.Contains(searchString)).ToList();

        return View(veiculos);
    }

    public IActionResult Buscar(string searchString)
    {
        if (string.IsNullOrEmpty(searchString))
        {
            // Se a string de busca for vazia, redirecione para a página inicial ou faça outra ação apropriada.
            return RedirectToAction("Index");
        }

        // Realize a busca com base na string de busca no campo 'MarcaModeloVeiculo'
        var veiculos = _context.Veiculo.Include(v => v.Fotos)
            .Where(v => v.MarcaModeloVeiculo.Contains(searchString))
            .ToList();

        return View("Index", veiculos);
    }

    [HttpGet]
    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Criar(Veiculo veiculo, List<IFormFile> fotos)
    {
        try
        {
            Console.WriteLine("Entrou no método Criar");

            if (ModelState.IsValid)
            {
                Console.WriteLine("ModelState é válido");

                // Lógica para processar e salvar as fotos
                if (fotos != null && fotos.Count > 0)
                {

                    Console.WriteLine("Passou aqui ");
                    foreach (var foto in fotos)
                    {
                        var novaFoto = new Foto();

                        // Use o serviço de armazenamento para salvar a foto
                        var imagePath = _fileStorageService.SaveFileAsync(foto, "uploads").Result;

                        Console.WriteLine($"Caminho da imagem salva: {imagePath}");
                        Console.WriteLine(foto.FileName);

                        // Adicione a nova foto à lista de fotos do veículo
                        novaFoto.NomeArquivo = foto.FileName;
                        veiculo.Fotos.Add(novaFoto);
                    }
                }

                // Lógica para salvar o veículo no banco de dados
                _context.Veiculo.Add(veiculo);
                _context.SaveChanges();

                Console.WriteLine("Veículo salvo no banco de dados");

                // Redirecionar para a página de detalhes ou outra ação conforme necessário
                TempData["MensagemSucesso"] = "Veículo salvo com sucesso!";
                return RedirectToAction("Detalhes", new { id = veiculo.Id });
            }


            // Se houver erros de validação, imprima os detalhes no console
            Console.WriteLine("ModelState não é válido. Erros de validação:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }
            // Se houver erros de validação
            TempData["MensagemErro"] = "Falha ao salvar o veículo. Verifique os erros de validação.";
            return View(veiculo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ocorreu uma exceção: {ex.Message}");
            Console.WriteLine($"StackTrace: {ex.StackTrace}");

            TempData["MensagemErro"] = "Ocorreu uma exceção ao criar o veículo.";
            return View("Error");
        }
    }

    public IActionResult Detalhes(int id)
    {
        var veiculo = _context.Veiculo.Include(v => v.Fotos) // Inclui as fotos relacionadas ao veículo
        .FirstOrDefault(v => v.Id == id);
        Console.WriteLine($"Número de fotos associadas ao veículo: {veiculo.Fotos.Count}");

        if (veiculo == null)
        {
            return NotFound();
        }

        return View(veiculo);
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {
        var veiculo = _context.Veiculo.Include(v => v.Fotos)
        .FirstOrDefault(v => v.Id == id);

        if (veiculo == null)
        {
            return NotFound();
        }

        return View(veiculo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Veiculo veiculo, List<IFormFile> novasFotos, List<string> fotosExistente, List<string> fotosRemover)
    {
        try
        {
            var veiculoExistente = _context.Veiculo.Include(v => v.Fotos).FirstOrDefault(v => v.Id == veiculo.Id);

            if (veiculoExistente == null)
            {
                return NotFound();
            }

            _context.Entry(veiculoExistente).CurrentValues.SetValues(veiculo);

            // Processa e salva novas fotos
            if (novasFotos != null && novasFotos.Count > 0)
            {
                foreach (var novaFoto in novasFotos)
                {
                    var fotoNova = new Foto();
                    var novaImagePath = _fileStorageService.SaveFileAsync(novaFoto, "uploads").Result;

                    fotoNova.NomeArquivo = novaFoto.FileName;
                    veiculoExistente.Fotos.Add(fotoNova);
                }
            }

            // Processa e remove fotos existentes
            if (fotosRemover != null && fotosRemover.Count > 0)
            {
                var fotosParaRemover = veiculoExistente.Fotos
                    .Where(f => fotosRemover.Contains(f.NomeArquivo))
                    .ToList();

                foreach (var fotoRemover in fotosParaRemover)
                {
                    veiculoExistente.Fotos.Remove(fotoRemover);
                    // Lógica para excluir fisicamente a foto do armazenamento, se necessário
                    _fileStorageService.DeleteFile(fotoRemover.NomeArquivo);
                }
            }

            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Veículo salvo com sucesso!";
            return RedirectToAction("Detalhes", new { id = veiculo.Id });
        }
        catch (Exception ex)
        {
            // Lógica para lidar com exceções, se necessário
            TempData["MensagemErro"] = "Ocorreu uma exceção ao editar o veículo.";
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult ConfirmarExclusao(int id)
    {
        var veiculo = _context.Veiculo.Find(id);

        if (veiculo == null)
        {
            return NotFound();
        }

        return View(veiculo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(int id)
    {
        var veiculo = _context.Veiculo.Find(id);

        if (veiculo == null)
        {
            return NotFound();
        }

        _context.Veiculo.Remove(veiculo);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }
}
