using CsvHelper.Configuration.Attributes;

namespace LogisticsAPI.Models;

// Klasa reprezentująca pojedynczy wiersz w pliku csv dotyczącym zapasów
public class InventoryCsvRow
{
    [Index(0)] // Atrybut Index z biblioteki CsvHelper jest używany do mapowania kolejności kolumn w pliku CSV na pola w klasie.
    public int ProductId { get; set; } // Identyfikator produktu
    
    [Index(1)]
    public string SKU { get; set; }
    
    [Index(2)]
    public string? Unit { get; set; } // Jednostka produktu może być null
    
    [Index(3)]
    public string Qty { get; set; }
    
    [Index(4)]
    public string? ManufacturerName { get; set; } // Nazwa producenta, może być null
    
    [Index(5)]
    public string? ManufacturerRefNum { get; set; } // Numer referencyjny producenta, może być null
    
    [Index(6)]
    public string Shipping { get; set; }
    
    [Index(7)]
    public decimal? ShippingCost { get; set; } // Koszt dostawy, może być null
}