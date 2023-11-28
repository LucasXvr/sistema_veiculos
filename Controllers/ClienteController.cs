using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

public class ClienteController : Controller
{
    private readonly ApplicationContext _context;
    private readonly ILogger<ClienteController> _logger;

    public ClienteController(ApplicationContext context, ILogger<ClienteController> logger)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _logger = logger;
    }

    public IActionResult Index(string searchString)
    {
        // Recupera a lista de clientes do banco de dados
        var clientes = _context.Cliente.ToList();

        // Aplica a filtragem se houver uma string de pesquisa
        if (!string.IsNullOrEmpty(searchString))
        {
            clientes = clientes.Where(c => c.Nome.Contains(searchString)).ToList();
        }

        // Passa a lista de clientes para a view
        return View(clientes);
    }

    [HttpGet]
    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Criar(Cliente cliente)
    {
        try
        {
            if (_context == null)
            {
                Console.WriteLine("_context é nulo!");
                throw new InvalidOperationException("_context não foi inicializado corretamente.");
            }

            if (ModelState.IsValid)
            {
                Console.WriteLine("Passou aqui ");
                _context.Add(cliente);
                _context.SaveChanges();

                Console.WriteLine("Veículo salvo no banco de dados");

                return RedirectToAction("Index");
            }
            else
            {
                // Log dos erros de validação
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"Erro de validação: {error.ErrorMessage}");
                }
            }

            Console.WriteLine("ModelState não é válido. Erros de validação:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }

            return View(cliente);
        }
        catch (Exception ex)
        {
            // Log do erro
            _logger.LogError($"Erro ao executar o método Criar: {ex.Message}");
            _logger.LogError($"StackTrace: {ex.StackTrace}");

            // Mensagem de erro mais detalhada para o usuário
            TempData["MensagemErro"] = $"Ocorreu um erro ao criar o veículo: {ex.Message}";

            // Redirecionar para a página de erro
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Detalhes(int id)
    {
        try
        {
            // Busca o cliente pelo ID
            var cliente = _context.Cliente.Find(id);

            // Verifica se o cliente foi encontrado
            if (cliente == null)
            {
                _logger.LogError($"Cliente com ID {id} não encontrado.");
                return View("NotFound"); // Ou redirecionar para uma página de erro específica
            }

            // Passa o cliente para a view
            return View("Detalhes", cliente);
        }
        catch (Exception ex)
        {
            // Log do erro
            _logger.LogError($"Erro ao executar o método Detalhes: {ex.Message}");
            _logger.LogError($"StackTrace: {ex.StackTrace}");

            // Mensagem de erro mais detalhada para o usuário
            TempData["MensagemErro"] = $"Ocorreu um erro ao obter os detalhes do cliente: {ex.Message}";

            // Redirecionar para a página de erro
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult Editar(int id)
    {

        var cliente = _context.Cliente.Find(id);

        // Verifica se o cliente foi encontrado
        if (cliente == null)
        {
            _logger.LogError($"Cliente com ID {id} não encontrado.");
            return View("NotFound"); // Ou redirecionar para uma página de erro específica
        }

        // Seu código aqui
        return View(cliente);

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, Cliente cliente)
    {
        try
        {
            if (_context == null)
            {
                Console.WriteLine("_context é nulo!");
                throw new InvalidOperationException("_context não foi inicializado corretamente.");
            }

            // Verifica se o ID fornecido é diferente do ID do cliente
            if (id != cliente.Id)
            {
                return NotFound(); // Retorna uma resposta 404 se os IDs não coincidirem
            }

            if (ModelState.IsValid)
            {
                var clienteExistente = _context.Cliente.Find(id);

                // Verifica se o cliente foi encontrado
                if (clienteExistente == null)
                {
                    _logger.LogError($"Cliente com ID {id} não encontrado.");
                    return View("NotFound"); // Ou redirecionar para uma página de erro específica
                }


                _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);

                _context.SaveChanges();

                Console.WriteLine("Veículo salvo no banco de dados");

                return RedirectToAction("Index");
            }
            else
            {
                // Log dos erros de validação
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError($"Erro de validação: {error.ErrorMessage}");
                }
            }

            Console.WriteLine("ModelState não é válido. Erros de validação:");
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine($"- {error.ErrorMessage}");
            }

            return View(cliente);
        }
        catch (Exception ex)
        {
            // Log do erro
            _logger.LogError($"Erro ao executar o método Detalhes: {ex.Message}");
            _logger.LogError($"StackTrace: {ex.StackTrace}");

            // Mensagem de erro mais detalhada para o usuário
            TempData["MensagemErro"] = $"Ocorreu um erro ao obter os detalhes do cliente: {ex.Message}";

            // Redirecionar para a página de erro
            return View("Error");
        }
    }

    [HttpGet]
    public IActionResult ConfirmarExclusao(int id)
    {
        var cliente = _context.Cliente.Find(id);

        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(int id)
    {
        try
        {
            Console.WriteLine("Passou Aqui");
            var cliente = _context.Cliente.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Cliente excluído com sucesso!";
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir o cliente: {Message}", ex.Message);
            TempData["MensagemErro"] = $"Erro ao excluir o cliente: {ex.Message}";
            return RedirectToAction("Index");
        }
    }
}