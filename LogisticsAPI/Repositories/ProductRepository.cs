using System.Data;
using LogisticsAPI.Database.Entities;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Repositories;

public class ProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration configuration)
    {
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
}