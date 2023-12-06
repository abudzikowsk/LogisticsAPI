namespace LogisticsAPI.Database.Entities;

// PriceEntity to klasa reprezentująca cenę produktu w bazie danych.
// Definiuje różne właściwości ceny, takie jak unikalny identyfikator, SKU produktu, cena netto, cena netto po uwzględnieniu zniżki, stawka VAT oraz cena netto z zniżką dla jednostki logistycznej.
public class PriceEntity
{
    public string Id { get; set; }
    public string SKU { get; set; }
    public decimal? PriceNett { get; set; }
    public decimal? PriceNettWithDiscount { get; set; }
    public int? VATRate { get; set; }
    public decimal? PriceNettWithDiscountForLogisticUnit { get; set; }
}