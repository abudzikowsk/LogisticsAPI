// Importowanie potrzebnych bibliotek
using LogisticsAPI.Database;
using LogisticsAPI.Repositories;
using LogisticsAPI.Services;

// Utworzenie instancji budowniczego aplikacji
var builder = WebApplication.CreateBuilder(args);

// Dodawanie usługi Http Client do kontenera DI
builder.Services.AddHttpClient();

// Rejestracja usług w kontenerze DI
builder.Services.AddScoped<DownloadService>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<MigrationService>();
builder.Services.AddScoped<CsvParserService>();
builder.Services.AddScoped<InventoryRepository>();
builder.Services.AddScoped<PriceRepository>();
builder.Services.AddScoped<ProductRepository>();

// Tworzenie instancji aplikacji
var app = builder.Build();

// Mapowanie endpointów na metody obsługiwane przez usługi
// Automaatyczne pobieranie i zapisywanie danych do bazy danych
app.MapGet("/DownloadAndSaveToDatabase", async (
    DownloadService downloadService, 
    CsvParserService csvParserService,
    InventoryRepository inventoryRepository,
    PriceRepository priceRepository,
    ProductRepository productRepository,
    MigrationService migrationService) =>
{
    // Uruchomienie migracji
    await migrationService.RunMigrations();
    
    // Pobieranie plików z danymi
    var productsPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Products.csv",
        "Products.csv");
    var inventoryPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Inventory.csv",
        "Inventory.csv");
    var pricesPath = downloadService.DownloadFileAsync(
        "https://rekturacjazadanie.blob.core.windows.net/zadanie/Prices.csv",
            "Prices.csv");
    
    // Czekanie na pobranie wszystkich plików
    Task.WaitAll(productsPath, inventoryPath, pricesPath);
    
    // Parsowanie plików CSV
    var priceEntities = await csvParserService.ParseCsvPrices(pricesPath.Result);
    var productEntities = await csvParserService.ParseCsvProduct(productsPath.Result);
    var inventoryEntities = await csvParserService.ParseCsvInventory(inventoryPath.Result);
    
    // Zapisywanie danych do bazy
    var insertPriceTask = priceRepository.InsertPrice(priceEntities);
    var insertProductTask = productRepository.InsertProduct(productEntities);
    
    // Czekanie na zakończenie zapisu
    Task.WaitAll(insertPriceTask, insertProductTask);
        
    //Musi wykonać się na końcu bo sa zależności do tej tablicy
    //Foreign key na Product table
    await inventoryRepository.InsertInventory(inventoryEntities);
    
    return "Ok";
});

// Ścieżka "/Product/{sku}"
//Endpoint do pobrania produktu po SKU
app.MapGet("/Product/{sku}", async (string sku, ProductRepository productRepository) =>
{
    var product = await productRepository.GetProductBySKU(sku);
    if (product == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(product);
});

// Uruchomienie aplikacji
app.Run();