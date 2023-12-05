IF ( NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Products'))
BEGIN
    CREATE TABLE Products(
                         Id INTEGER PRIMARY KEY,
                         SKU VARCHAR(255),
                         Name VARCHAR(255) NULL,
                         EAN VARCHAR(255) NULL,
                         ProducerName VARCHAR(255) NULL,
                         Category VARCHAR(255) NULL,
                         IsWire BIT NULL,
                         Available BIT NULL,
                         IsVendor BIT NULL,
                         DefaultImage VARCHAR(255) NULL
    );
END

IF ( NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Inventories'))
BEGIN
    CREATE TABLE Inventories(
                          ProductId INT REFERENCES Products(Id),
                          SKU VARCHAR(255),
                          Unit VARCHAR(255) NULL,
                          Qty INT,
                          ManufacturerName VARCHAR(255) NULL,
                          ManufacturerRefNum VARCHAR(255) NULL,
                          Shipping VARCHAR(255),
                          ShippingCost DECIMAL(18, 2) NULL
    );
END

IF ( NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Prices'))
BEGIN
    CREATE TABLE Prices(
                      Id INTEGER PRIMARY KEY,
                      SKU VARCHAR(255),
                      PriceNett DECIMAL(18, 2) NULL,
                      PriceNettWithDiscount DECIMAL(18, 2) NULL,
                      VATRate INTEGER NULL,
                      PriceNettWithDiscountForLogisticUnit DECIMAL(18, 2) NULL
    );
END
