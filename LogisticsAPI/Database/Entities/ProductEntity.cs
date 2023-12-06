namespace LogisticsAPI.Database.Entities;

// ProductEntity to klasa reprezentująca produkt w bazie danych.
// Definiuje różne właściwości produktu, takie jak unikalny identyfikator, SKU, nazwa produktu, kod EAN, nazwa producenta, kategoria, czy jest przewodem, czy jest dostępny, czy jest dostawcą i domyślny obrazek.
public class ProductEntity
{
    public int Id { get; set; }
    public string SKU { get; set; }
    public string? Name { get; set; }
    public string? EAN { get; set; }
    public string? ProducerName { get; set; }
    public string? Category { get; set; }
    public bool? IsWire { get; set; }
    public bool? Available { get; set; }
    public bool? IsVendor { get; set; }
    public string? DefaultImage { get; set; }
}