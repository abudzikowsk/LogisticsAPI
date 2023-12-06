using System.Data;
using Dapper;
using LogisticsAPI.Database;
using LogisticsAPI.Database.Entities;
using LogisticsAPI.ViewModels;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Repositories;

// Klasa reprezentująca repozytorium dla produktów
public class ProductRepository
{
    private readonly DapperContext _dapperContext; // Kontekst dla Dappera
    private readonly string _connectionString; // Ciąg połączenia do bazy danych

    // Konstruktor repozytorium produktów
    public ProductRepository(IConfiguration configuration, DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
        _connectionString = configuration.GetConnectionString("DefaultConnection"); // Pobieranie ciągu połączenia do bazy danych
    }
    
    // Metoda do wstawiania grupy obiektów produktu do bazy danych
    public async Task InsertProduct(List<ProductEntity> productEntities)
    {
        var table = new DataTable(); // Tworzenie tabeli DataTable
        
        // Dodawanie kolumn do tabeli
        table.Columns.Add("Id", typeof(int));
        table.Columns.Add("SKU", typeof(string));
        table.Columns.Add("Name", typeof(string));
        table.Columns.Add("EAN", typeof(string));
        table.Columns.Add("ProducerName", typeof(string));
        table.Columns.Add("Category", typeof(string));
        table.Columns.Add("IsWire", typeof(bool));
        table.Columns.Add("Available", typeof(bool));
        table.Columns.Add("IsVendor", typeof(bool));
        table.Columns.Add("DefaultImage", typeof(string));

        // Wypełnianie tabeli danymi
        foreach (var productEntity in productEntities)
        {
            table.Rows.Add(productEntity.Id, productEntity.SKU, productEntity.Name, productEntity.EAN,
                productEntity.ProducerName, productEntity.Category, productEntity.IsWire, productEntity.Available,
                productEntity.IsVendor, productEntity.DefaultImage);
        }
        
        // Użycie SqlBulkCopy do szybkiego kopiowania danych
        using var sqlBulk = new SqlBulkCopy(_connectionString);
        sqlBulk.DestinationTableName = "Products"; // Definiowanie, do której tabeli będą kopiowane dane
        await sqlBulk.WriteToServerAsync(table); // Kopiowanie danych do serwera SQL
    }
    
    // Metoda do pobierania produktu na podstawie SKU
    public async Task<ProductViewModel?> GetProductBySKU(string sku)
    {
        var query = """
                    SELECT
                        p.Name,
                        p.EAN,
                        i.ManufacturerName,
                        p.Category,
                        p.DefaultImage,
                        i.Qty,
                        i.Unit,
                        pr.PriceNett,
                        i.ShippingCost
                    FROM Products p
                    INNER JOIN dbo.Inventories i on p.Id = i.ProductId
                    INNER JOIN dbo.Prices pr on pr.SKU = p.SKU
                    WHERE p.SKU = @sku
                    """;
    
        using var connection = _dapperContext.CreateConnection(); // Utworzenie połączenia
        return await connection.QuerySingleOrDefaultAsync<ProductViewModel>(query, new {sku}); // Wykonanie zapytania i zwrócenie wyniku
    }
}