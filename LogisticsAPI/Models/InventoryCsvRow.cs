using CsvHelper.Configuration.Attributes;

namespace LogisticsAPI.Models;

public class InventoryCsvRow
{
    [Index(0)]
    public int ProductId { get; set; }
    
    [Index(1)]
    public string SKU { get; set; }
    
    [Index(2)]
    public string? Unit { get; set; }
    
    [Index(3)]
    public string Qty { get; set; }
    
    [Index(4)]
    public string? ManufacturerName { get; set; }
    
    [Index(5)]
    public string? ManufacturerRefNum { get; set; }
    
    [Index(6)]
    public string Shipping { get; set; }
    
    [Index(7)]
    public decimal? ShippingCost { get; set; }
}