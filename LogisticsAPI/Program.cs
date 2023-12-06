using Dapper;
using LogisticsAPI.Database;
using LogisticsAPI.Repositories;
using LogisticsAPI.Services;
using LogisticsAPI.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
builder.Services.AddScoped<DownloadService>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<MigrationService>();
builder.Services.AddScoped<CsvParserService>();
builder.Services.AddScoped<InventoryRepository>();
builder.Services.AddScoped<PriceRepository>();
builder.Services.AddScoped<ProductRepository>();
var app = builder.Build();

app.MapGet("/DownloadAndSaveToDatabase", async (
    DownloadService downloadService, 
    CsvParserService csvParserService,
    InventoryRepository inventoryRepository,
    PriceRepository priceRepository,
    ProductRepository productRepository,
    MigrationService migrationService) =>
{
    await migrationService.RunMigrations();
    
    var productsPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv",
        "Products.csv");
    var inventoryPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv",
        "Inventory.csv");
    var pricesPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv",
            "Prices.csv");
    
    Task.WaitAll(productsPath, inventoryPath, pricesPath);
    
    var priceEntities = await csvParserService.ParseCsvPrices(pricesPath.Result);
    var productEntities = await csvParserService.ParseCsvProduct(productsPath.Result);
    var inventoryEntities = await csvParserService.ParseCsvInventory(inventoryPath.Result);
    
    var insertPriceTask = priceRepository.InsertPrice(priceEntities);
    var insertProductTask = productRepository.InsertProduct(productEntities);
    Task.WaitAll(insertPriceTask, insertProductTask);
        
    //Musi wykonac sie na koncu bo sa zaleznosci do tej tablicy. Foreign key na Product 
    await inventoryRepository.InsertInventory(inventoryEntities);
    
    return "Ok";
});

app.MapGet("/Product/{sku}", async (string sku, ProductRepository productRepository) =>
{
    var product = await productRepository.GetProductBySKU(sku);
    if (product == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(product);
});

app.Run();