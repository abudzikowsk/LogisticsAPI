using CsvHelper.Configuration.Attributes;

namespace LogisticsAPI.Models;

public class PriceCsvRow
{
    [Index(0)]
    public string Id { get; set; }
    
    [Index(1)]
    public string SKU { get; set; }
    
    [Index(2)]
    public decimal? PriceNett { get; set; }
    
    [Index(3)]
    public decimal? PriceNettWithDiscount { get; set; }
    
    [Index(4)]
    public string? VATRate { get; set; }
    
    [Index(5)]
    public decimal? PriceNettWithDiscountForLogisticUnit { get; set; }
}