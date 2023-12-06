using System.Data;
using LogisticsAPI.Database.Entities;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Repositories;

public class InventoryRepository
{
    private readonly string _connectionString;

    public InventoryRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task InsertInventory(List<InventoryEntity> inventoryEntities)
    {
        var table = new DataTable();
        table.Columns.Add("ProductId", typeof(int));
        table.Columns.Add("SKU", typeof(string));
        table.Columns.Add("Unit", typeof(string));
        table.Columns.Add("Qty", typeof(int));
        table.Columns.Add("ManufacturerName", typeof(string));
        table.Columns.Add("ManufacturerRefNum", typeof(string));
        table.Columns.Add("Shipping", typeof(string));
        table.Columns.Add("ShippingCost", typeof(string));

        foreach (var inventoryEntity in inventoryEntities)
        {
            table.Rows.Add(inventoryEntity.ProductId, inventoryEntity.SKU, inventoryEntity.Unit, inventoryEntity.Qty,
                inventoryEntity.ManufacturerName, inventoryEntity.ManufacturerRefNum, inventoryEntity.Shipping,
                inventoryEntity.ShippingCost);
        }

        using var sqlBulk = new SqlBulkCopy(_connectionString);
        sqlBulk.DestinationTableName = "Inventories";
        await sqlBulk.WriteToServerAsync(table);
    }
}