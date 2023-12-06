using System.Data;
using LogisticsAPI.Database.Entities;
using Microsoft.Data.SqlClient;

namespace LogisticsAPI.Repositories;

public class PriceRepository
{
    private readonly string _connectionString;

    public PriceRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task InsertPrice(List<PriceEntity> priceEntities)
    {
        var table = new DataTable();
        table.Columns.Add("Id", typeof(string));
        table.Columns.Add("SKU", typeof(string));
        table.Columns.Add("PriceNett", typeof(decimal));
        table.Columns.Add("PriceNettWithDiscount", typeof(decimal));
        table.Columns.Add("VATRate", typeof(int));
        table.Columns.Add("PriceNettWithDiscountForLogisticUnit", typeof(decimal));

        foreach (var priceEntity in priceEntities)
        {
            table.Rows.Add(priceEntity.Id, priceEntity.SKU, priceEntity.PriceNett, priceEntity.PriceNettWithDiscount, priceEntity.VATRate, priceEntity.PriceNettWithDiscountForLogisticUnit);
        }

        using var sqlBulk = new SqlBulkCopy(_connectionString);
        sqlBulk.DestinationTableName = "Prices";
        await sqlBulk.WriteToServerAsync(table);
    }
}