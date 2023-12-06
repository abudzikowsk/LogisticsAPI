using System.Data;
using Dapper;
using LogisticsAPI.Database;
using LogisticsAPI.Database.Entities;
using LogisticsAPI.ViewModels;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Repositories;

public class ProductRepository
{
    private readonly DapperContext _dapperContext;
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration, DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task InsertProduct(List<ProductEntity> productEntities)
    {
        var table = new DataTable();
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

        foreach (var productEntity in productEntities)
        {
            table.Rows.Add(productEntity.Id, productEntity.SKU, productEntity.Name, productEntity.EAN,
                productEntity.ProducerName, productEntity.Category, productEntity.IsWire, productEntity.Available,
                productEntity.IsVendor, productEntity.DefaultImage);
        }
        
        using var sqlBulk = new SqlBulkCopy(_connectionString);
        sqlBulk.DestinationTableName = "Products";
        await sqlBulk.WriteToServerAsync(table);
    }

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
    
        using var connection = _dapperContext.CreateConnection();
        return await connection.QuerySingleOrDefaultAsync<ProductViewModel>(query, new {sku});
    }
}