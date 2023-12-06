using CsvHelper.Configuration.Attributes;

namespace LogisticsAPI.Models;

// Klasa reprezentująca pojedynczy wiersz w pliku CSV dotyczącym produktu
public class ProductCsvRow
{
    [Index(0)] // Atrybut Index z biblioteki CsvHelper jest używany do mapowania kolejności kolumn w pliku CSV na pola w klasie.
    public int Id { get; set; } // Identyfikator produktu
    
    [Index(1)]
    public string SKU { get; set; }
    
    [Index(2)]
    public string? Name { get; set; } // Nazwa produktu, może być null
    
    [Index(4)]
    public string? EAN { get; set; } // Kod EAN produktu, może być null
    
    [Index(6)]
    public string? ProducerName { get; set; } // Nazwa producenta, może być null
    
    [Index(7)]
    public string? Category { get; set; } // Kategoria produktu, może być null
    
    [Index(8)]
    public bool? IsWire { get; set; } // Informacja, czy produkt jest przewodem, może być null
    
    [Index(11)]
    public bool? Available { get; set; } // Informacja, czy produkt jest dostępny, może być null
    
    [Index(16)]
    public bool? IsVendor { get; set; } // Informacja, czy produkt jest od dostawcy, może być null
    
    [Index(18)]
    public string? DefaultImage { get; set; } // Domyślny obrazek produktu, może być null
}