using System.Data;
using LogisticsAPI.Database.Entities;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Repositories;

// Klasa reprezentująca repozytorium dla zapasów
public class InventoryRepository
{
    private readonly string _connectionString; // Ciąg połączenia do bazy danych
    public InventoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection"); // Pobieranie ciągu połączenia do bazy danych
    }

    public async Task InsertInventory(List<InventoryEntity> inventoryEntities)
    {
        var table = new DataTable();// Tworzenie tabeli DataTable
        
        // Dodawanie kolumn do tabeli
        table.Columns.Add("ProductId", typeof(int));
        table.Columns.Add("SKU", typeof(string));
        table.Columns.Add("Unit", typeof(string));
        table.Columns.Add("Qty", typeof(int));
        table.Columns.Add("ManufacturerName", typeof(string));
        table.Columns.Add("ManufacturerRefNum", typeof(string));
        table.Columns.Add("Shipping", typeof(string));
        table.Columns.Add("ShippingCost", typeof(string));

        // Wypełnianie tabeli danymi
        foreach (var inventoryEntity in inventoryEntities)
        {
            table.Rows.Add(inventoryEntity.ProductId, inventoryEntity.SKU, inventoryEntity.Unit, inventoryEntity.Qty,
                inventoryEntity.ManufacturerName, inventoryEntity.ManufacturerRefNum, inventoryEntity.Shipping,
                inventoryEntity.ShippingCost);
        }

        // Użycie SqlBulkCopy do szybkiego kopiowania danych
        using var sqlBulk = new SqlBulkCopy(_connectionString); 
        sqlBulk.DestinationTableName = "Inventories"; // Ustawienie tabeli docelowej dla SqlBulkCopy
        await sqlBulk.WriteToServerAsync(table); // Zapisanie danych z tabeli do serwera SQL
    }
}