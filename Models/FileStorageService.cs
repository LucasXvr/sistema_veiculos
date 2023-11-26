public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string subfolder);
    Task<byte[]> GetFileAsync(string filePath);
    void DeleteFile(string filePath);
}

public class FileStorageService : IFileStorageService
{
    private readonly string _uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

    public async Task<string> SaveFileAsync(IFormFile file, string subfolder)
    {
        try
        {
            var folderPath = Path.Combine(_uploadsPath, subfolder);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = Path.Combine(folderPath, Path.GetFileName(file.FileName));

            using (var fileStream = new FileStream(fileName, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            Console.WriteLine($"Imagem salva com sucesso: {fileName}");

            return Path.Combine(subfolder, Path.GetFileName(file.FileName));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar imagem: {ex.Message}");
            throw; // Re-throw a exceção para que ela seja capturada no código cliente
        }
    }

    public async Task<byte[]> GetFileAsync(string filePath)
    {
        var fullPath = Path.Combine(_uploadsPath, filePath);

        if (File.Exists(fullPath))
            return await File.ReadAllBytesAsync(fullPath);

        return null;
    }

    public void DeleteFile(string filePath)
    {
        var fullPath = Path.Combine(_uploadsPath, filePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            DeleteEmptyDirectories(Path.GetDirectoryName(fullPath));
        }
    }

    private void DeleteEmptyDirectories(string directory)
    {
        if (Directory.Exists(directory) && !Directory.EnumerateFileSystemEntries(directory).Any())
        {
            Directory.Delete(directory);
            DeleteEmptyDirectories(Path.GetDirectoryName(directory));
        }
    }
}


