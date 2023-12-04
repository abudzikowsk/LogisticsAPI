using LogisticsAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<DownloadService>();
var app = builder.Build();

app.MapGet("/DownloadAndSaveToDatabase", (DownloadService downloadService) =>
{
    var productsPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv",
        "Product.csv");
    var inventoryPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv",
        "Inventory.csv");
    var pricesPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv",
            "Prices.csv");
    
    Task.WaitAll(productsPath, inventoryPath, pricesPath);
});

app.Run();