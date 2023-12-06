namespace LogisticsAPI.ViewModels;

//Klasa ProductViewModel jest wykorzystywana do reprezentacji widoku produktu.
//Zawiera właściwości takie jak nazwa produktu, kod EAN, nazwa producenta, kategoria, domyślny obraz, ilość w magazynie, jednostka miary, cena netto i koszt wysyłki.
public class ProductViewModel
{
    public string? Name { get; set; } //Product
    public string? EAN { get; set; } //Product
    public string? ManufacturerName { get; set; } // Inventory
    public string? Category { get; set; } //Product
    public string? DefaultImage { get; set; } //Product
    public int Qty { get; set; } // Inventory
    public string? Unit { get; set; } // Inventory
    public decimal? PriceNett { get; set; } //Price
    public decimal? ShippingCost { get; set; } //Inventory
}