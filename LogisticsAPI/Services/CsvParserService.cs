using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using LogisticsAPI.Database.Entities;
using LogisticsAPI.Models;

namespace LogisticsAPI.Services;

public class CsvParserService
{
    public async Task<List<PriceEntity>> ParseCsvPrices(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        };
        using var csv = new CsvReader(reader, csvConfiguration);
        
        List<PriceEntity> priceEntities = new List<PriceEntity>();
        if (await csv.ReadAsync())
        {
            var records = csv.GetRecords<PriceCsvRow>();
            foreach (var record in records)
            {
                var isVatParsed = int.TryParse(record.VATRate, out var parsedVatRate);
                priceEntities.Add(new PriceEntity
                {
                    Id = record.Id,
                    PriceNett = record.PriceNett,
                    SKU = record.SKU,
                    PriceNettWithDiscount = record.PriceNettWithDiscount,
                    VATRate = isVatParsed ? parsedVatRate : 0,
                    PriceNettWithDiscountForLogisticUnit = record.PriceNettWithDiscountForLogisticUnit
                });
            }
        }

        return priceEntities;
    }
    
    public async Task<List<ProductEntity>> ParseCsvProduct(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = true,
            MissingFieldFound = null,
            HeaderValidated = null,
            ReadingExceptionOccurred = context => false
        };
        using var csv = new CsvReader(reader, csvConfiguration);

        List<ProductEntity> productEntities = new List<ProductEntity>();
        if (await csv.ReadAsync())
        {
            var records = csv.GetRecords<ProductCsvRow>();
            foreach (var record in records)
            {
                productEntities.Add(new ProductEntity
                {
                    Id = record.Id,
                    SKU = record.SKU,
                    Available = record.Available,
                    Category = record.Category,
                    Name = record.Name,
                    DefaultImage = record.DefaultImage,
                    IsVendor = record.IsVendor,
                    IsWire = record.IsWire,
                    ProducerName = record.ProducerName,
                    EAN = record.EAN
                });
            }
        }

        return productEntities;
    }
    
    public async Task<List<InventoryEntity>> ParseCsvInventory(string filePath)
    {
        using var reader = new StreamReader(filePath);
        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            HeaderValidated = null
        };
        using var csv = new CsvReader(reader, csvConfiguration);

        List<InventoryEntity> inventoryEntities = new List<InventoryEntity>();
        if (await csv.ReadAsync())
        {
            var records = csv.GetRecords<InventoryCsvRow>();
            foreach (var record in records)
            {
                var isQtyParsed = int.TryParse(record.Qty, out var parsedQty);
                inventoryEntities.Add(new InventoryEntity
                {
                    Qty = isQtyParsed ? parsedQty : 0,
                    SKU = record.SKU,
                    Shipping = record.Shipping,
                    Unit = record.Unit,
                    ManufacturerName = record.ManufacturerName,
                    ProductId = record.ProductId,
                    ShippingCost = record.ShippingCost,
                    ManufacturerRefNum = record.ManufacturerRefNum
                });
            }
        }

        return inventoryEntities;
    }
}