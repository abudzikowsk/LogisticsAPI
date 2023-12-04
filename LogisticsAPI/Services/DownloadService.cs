namespace LogisticsAPI.Services;

public class DownloadService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public DownloadService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> DownloadFileAsync(string url, string fileName)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        var response = await httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStreamAsync();
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            await using var fileStream = new FileStream(filePath, FileMode.Create);
            await responseStream.CopyToAsync(fileStream);
            fileStream.Close();
            
            return filePath;
        }
        
        return string.Empty;
    }
}