namespace LogisticsAPI.Database.Entities;

public class InventoryEntity
{
    public int ProductId { get; set; }
    public string SKU { get; set; }
    public string? Unit { get; set; }
    public int Qty { get; set; }
    public string? ManufacturerName { get; set; }
    public string? ManufacturerRefNum { get; set; }
    public string Shipping { get; set; }
    public decimal? ShippingCost { get; set; }
}