namespace LogisticsAPI.Database.Entities;

//InventoryEntity to klasa reprezentująca stan magazynu produktu w bazie danych.
//Definiuje różne właściwości związane z magazynowaniem produktu, jak identyfikator produktu, SKU produktu, jednostka produktu, ilość produktu w magazynie, nazwa producenta, numer referencyjny producenta, metoda wysyłki produktu i koszt wysyłki.
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