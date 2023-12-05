using LogisticsAPI.Database;
using LogisticsAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<DownloadService>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<MigrationService>();
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

app.MapGet("/RunMigrations", async (MigrationService migrationService) =>
{
    await migrationService.RunMigrations();
});

app.Run();