using CsvHelper.Configuration.Attributes;

namespace LogisticsAPI.Models;

public class ProductCsvRow
{
    [Index(0)]
    public int Id { get; set; }
    
    [Index(1)]
    public string SKU { get; set; }
    
    [Index(2)]
    public string? Name { get; set; }
    
    [Index(4)]
    public string? EAN { get; set; }
    
    [Index(6)]
    public string? ProducerName { get; set; }
    
    [Index(7)]
    public string? Category { get; set; }
    
    [Index(8)]
    public bool? IsWire { get; set; }
    
    [Index(11)]
    public bool? Available { get; set; }
    
    [Index(16)]
    public bool? IsVendor { get; set; }
    
    [Index(18)]
    public string? DefaultImage { get; set; }
}