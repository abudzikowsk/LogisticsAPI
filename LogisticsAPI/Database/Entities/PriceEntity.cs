namespace LogisticsAPI.Database.Entities;

public class PriceEntity
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public decimal? PriceNett { get; set; }
    public decimal? PriceNettWithDiscount { get; set; }
    public int? VATRate { get; set; }
    public decimal? PriceNettWithDiscountForLogisticUnit { get; set; }
}