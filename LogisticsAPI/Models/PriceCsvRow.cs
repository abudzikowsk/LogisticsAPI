using CsvHelper.Configuration.Attributes;

namespace LogisticsAPI.Models;

// Klasa reprezentuje pojedynczy wiersz w pliku csv dotyczącym cen
public class PriceCsvRow
{
    [Index(0)] // Atrybut Index z biblioteki CsvHelper jest używany do mapowania kolejności kolumn w pliku CSV na pola w klasie.
    public string Id { get; set; } // Identyfikator ceny
    
    [Index(1)]
    public string SKU { get; set; }
    
    [Index(2)]
    public decimal? PriceNett { get; set; } // Cena netto produktu, może być null
    
    [Index(3)]
    public decimal? PriceNettWithDiscount { get; set; } // Cena netto produktu po uwzględnieniu rabatu, może być null
    
    [Index(4)]
    public string? VATRate { get; set; } // Stawka VAT, może być null
    
    [Index(5)]
    public decimal? PriceNettWithDiscountForLogisticUnit { get; set; } // Cena netto z rabatem dla jednostki logistycznej, może być null
}