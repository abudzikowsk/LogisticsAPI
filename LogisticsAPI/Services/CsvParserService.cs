using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using LogisticsAPI.Database.Entities;
using LogisticsAPI.Models;

namespace LogisticsAPI.Services;

// Klasa obsługująca parsowanie plików CSV
public class CsvParserService
{
    // Metoda parsująca plik CSV, które zawiera informacje o cenach produktów
    public async Task<List<PriceEntity>> ParseCsvPrices(string filePath)
    {
        using var reader = new StreamReader(filePath); // Utworzenie czytnika pliku CSV
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture) // Utworzenie konfiguracji CsvReader
        {
            HasHeaderRecord = false
        };
        using var csv = new CsvReader(reader, csvConfiguration); // Utworzenie CsvReader
        
        List<PriceEntity> priceEntities = new List<PriceEntity>(); // Inicjalizacja listy z encjami cen
        
        // Czytanie nagłówka pliku CSV
        if (await csv.ReadAsync())
        {
            // Przetwarzanie wierszy w pliku i zapisywanie ich jako PriceCsvRow
            // PriceCsvRow to klasa reprezentująca pojedynczy wiersz w pliku CSV dotyczącym cen
            var records = csv.GetRecords<PriceCsvRow>();
            
            // Przechodzenie przez wszystkie pobrane rekordy
            foreach (var record in records)
            {
                // Próba parsowania wartości VAT do liczby
                var isVatParsed = int.TryParse(record.VATRate, out var parsedVatRate);
                
                // Tworzenie nowej encji ceny i dodawanie jej do listy
                priceEntities.Add(new PriceEntity
                {
                    Id = record.Id,
                    PriceNett = record.PriceNett,
                    SKU = record.SKU,
                    PriceNettWithDiscount = record.PriceNettWithDiscount,
                    VATRate = isVatParsed ? parsedVatRate : 0,
                    PriceNettWithDiscountForLogisticUnit = record.PriceNettWithDiscountForLogisticUnit
                });
            }
        }

        return priceEntities; // Zwracanie listy przetworzonych encji cen
    }
    
    // Metoda parsująca plik CSV, które zawiera informacje o produktach
    public async Task<List<ProductEntity>> ParseCsvProduct(string filePath)
    {
        // Tworzenie nowego StreamReadera, do odczytywania plików
        using var reader = new StreamReader(filePath);
        // Konfiguracja pliku CSV
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Separator pola to średnik
            Delimiter = ";", 
            // Plik zawiera wiersz nagłówka
            HasHeaderRecord = true, 
            // Ignoruje brakujące pola
            MissingFieldFound = null, 
            // Ignoruje problematyczne nagłówki
            HeaderValidated = null,
            // Nie rzuca wyjątkiem podczas błędów odczytu
            ReadingExceptionOccurred = context => false 
        };
        
        // Tworzenie nowego CsvReadera, do czytania plików CSV
        using var csv = new CsvReader(reader, csvConfiguration);

        // Inicjalizowanie listy na obiekty typu ProductEntity
        List<ProductEntity> productEntities = new List<ProductEntity>();
        
        // Jeśli udało się przeczytać plik
        if (await csv.ReadAsync())
        {
            // Przetwarzanie wierszy w pliku i zapisywanie ich jako ProductCsvRow
            // ProductCsvRow to klasa reprezentująca pojedynczy wiersz w pliku CSV dotyczącym produktów
            var records = csv.GetRecords<ProductCsvRow>();
            
            foreach (var record in records)
            {
                // Dodawanie nowego obiektu ProductEntity do listy
                productEntities.Add(new ProductEntity
                {
                    // Przypisywanie wartości z rekordu do odpowiednich pól obiektu ProductEntity
                    Id = record.Id,
                    SKU = record.SKU,
                    Available = record.Available,
                    Category = record.Category,
                    Name = record.Name,
                    DefaultImage = record.DefaultImage,
                    IsVendor = record.IsVendor,
                    IsWire = record.IsWire,
                    ProducerName = record.ProducerName,
                    EAN = record.EAN
                });
            }
        }
        
        // Zwracanie listy obiektów typu ProductEntity
        return productEntities;
    }
    
    public async Task<List<InventoryEntity>> ParseCsvInventory(string filePath)
    {
        // Tworzenie nowego StreamReadera, do czytania pliku podanej ścieżce
        using var reader = new StreamReader(filePath);
        
        // Konfiguracja CSV, informuje CsvReadera, jak interpretować plik CSV
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            // Plik zawiera wiersz nagłówka
            HasHeaderRecord = true,
            // Ignoruje brakujące pola
            MissingFieldFound = null,
            // Ignoruje problematyczne nagłówki
            HeaderValidated = null
        };
        
        // Tworzenie CsvReadera, do czytania plików w formacie CSV
        using var csv = new CsvReader(reader, csvConfiguration);

        // Inicjalizowanie listy dla przechowywania produktów
        List<InventoryEntity> inventoryEntities = new List<InventoryEntity>();
        
        // Jeśli udało się przeczytać plik
        if (await csv.ReadAsync())
        {
            // Przetwarzanie kolejnych wierszy pliku jako typu InventoryCsvRow
            // InventoryCsvRow to klasa reprezentująca pojedynczy wiersz w pliku CSV dotyczącym zapasów
            var records = csv.GetRecords<InventoryCsvRow>();
            
            foreach (var record in records)
            {
                // Próbuje sparsować ilość jako liczbę całkowitą
                var isQtyParsed = int.TryParse(record.Qty, out var parsedQty);
                // Dodawanie nowego elementu do listy produktów
                inventoryEntities.Add(new InventoryEntity
                {
                    // Tworzenie nowej encji ceny i dodawanie jej do listy
                    Qty = isQtyParsed ? parsedQty : 0, // Przypisywanie ilości tylko jeśli parsowanie się powiodło, w przeciwnym wypadku 0
                    SKU = record.SKU,
                    Shipping = record.Shipping,
                    Unit = record.Unit,
                    ManufacturerName = record.ManufacturerName,
                    ProductId = record.ProductId,
                    ShippingCost = record.ShippingCost,
                    ManufacturerRefNum = record.ManufacturerRefNum
                });
            }
        }

        // Zwracanie przetworzonych danych
        return inventoryEntities;
    }
}