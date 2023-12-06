using System.Data;
using LogisticsAPI.Database.Entities;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Repositories;

// Klasa reprezentująca repozytorium dla cen
public class PriceRepository
{
    private readonly string _connectionString; // Ciąg połączenia do bazy danych

    // Konstruktor repozytorium cen
    public PriceRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection"); // Pobranie ciągu połączenia do bazy danych
    }
    
    // Metoda do wstawiania cen do bazy danych
    public async Task InsertPrice(List<PriceEntity> priceEntities)
    {
        var table = new DataTable(); // Tworzenie nowej tabeli
        
        // Definiowanie kolumn tabeli
        table.Columns.Add("Id", typeof(string));
        table.Columns.Add("SKU", typeof(string));
        table.Columns.Add("PriceNett", typeof(decimal));
        table.Columns.Add("PriceNettWithDiscount", typeof(decimal));
        table.Columns.Add("VATRate", typeof(int));
        table.Columns.Add("PriceNettWithDiscountForLogisticUnit", typeof(decimal));

        // Wypełnienie tabeli danymi z listy entity ceny
        foreach (var priceEntity in priceEntities)
        {
            table.Rows.Add(priceEntity.Id, priceEntity.SKU, priceEntity.PriceNett, priceEntity.PriceNettWithDiscount, priceEntity.VATRate, priceEntity.PriceNettWithDiscountForLogisticUnit);
        }

        // Użycie SqlBulkCopy do szybkiego kopiowania danych
        using var sqlBulk = new SqlBulkCopy(_connectionString);
        sqlBulk.DestinationTableName = "Prices"; // Określenie tabeli docelowej
        await sqlBulk.WriteToServerAsync(table); // Zapis danych do serwera
    }
}