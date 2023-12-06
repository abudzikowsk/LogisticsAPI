namespace LogisticsAPI.Services;

// Klasa do pobierania plików
public class DownloadService
{
    // Fabryka klientów HTTP, umożliwia tworzenie instancji HttpClient
    private readonly IHttpClientFactory _httpClientFactory;

    // Konstruktor, który otrzymuje IHttpClientFactory jako parametr
    public DownloadService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Asynchroniczna metoda do pobierania plików
    public async Task<string> DownloadFileAsync(string url, string fileName)
    {
        // Utworzenie klienta HTTP
        using var httpClient = _httpClientFactory.CreateClient();
        // Wysłanie żądania GET do określonego adresu URL
        var response = await httpClient.GetAsync(url);

        // Sprawdzenie, czy żądanie się powiodło
        if (response.IsSuccessStatusCode)
        {
            // Pobranie zawartości ciała odpowiedzi jako strumienia
            var responseStream = await response.Content.ReadAsStreamAsync();
            // Tworzenie ścieżki do pliku
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            // Tworzenie strumienia do zapisu pliku
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            // Kopiowanie strumienia odpowiedzi do strumienia pliku
            await responseStream.CopyToAsync(fileStream);
            // Zamykanie strumienia pliku
            fileStream.Close();
            
            // Zwracanie ścieżki do pliku
            return filePath;
        }
        
        // Zwracanie pustego ciąga znaków, jeśli żądanie nie powiodło się
        return string.Empty;
    }
}