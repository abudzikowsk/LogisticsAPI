-- Sprawdź, czy istnieje tabela 'Products'
IF ( NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Products'))
BEGIN
    -- Jeżeli nie istnieje, utwórz tabelę 'Products'
    CREATE TABLE Products(
                         Id INTEGER PRIMARY KEY, -- Klucz główny
                         SKU VARCHAR(255),
                         Name VARCHAR(1000) NULL,
                         EAN VARCHAR(255) NULL,
                         ProducerName VARCHAR(255) NULL,
                         Category VARCHAR(1000) NULL,
                         IsWire BIT NULL,
                         Available BIT NULL,
                         IsVendor BIT NULL,
                         DefaultImage VARCHAR(1000) NULL
    );
END

-- Sprawdź, czy istnieje tabela 'Inventories'
IF ( NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Inventories'))
BEGIN
    -- Jeżeli nie istnieje, utwórz tabelę 'Inventories' 
    CREATE TABLE Inventories(
                          ProductId INT REFERENCES Products(Id), -- Klucz obcy odnoszący się do produktu
                          SKU VARCHAR(255),
                          Unit VARCHAR(255) NULL,
                          Qty INT,
                          ManufacturerName VARCHAR(255) NULL,
                          ManufacturerRefNum VARCHAR(255) NULL,
                          Shipping VARCHAR(255),
                          ShippingCost DECIMAL(18, 2) NULL
    );
END

-- Sprawdź, czy istnieje tabela 'Prices'
IF ( NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_NAME = 'Prices'))
BEGIN
    -- Jeżeli nie istnieje, utwórz tabelę 'Prices'
    CREATE TABLE Prices(
                      Id VARCHAR(255) PRIMARY KEY, -- Klucz główny
                      SKU VARCHAR(255),
                      PriceNett DECIMAL(18, 2) NULL,
                      PriceNettWithDiscount DECIMAL(18, 2) NULL,
                      VATRate INTEGER NULL,
                      PriceNettWithDiscountForLogisticUnit DECIMAL(18, 2) NULL
    );
END
