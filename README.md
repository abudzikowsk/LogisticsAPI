# Project: REST API with .NET 7, C#, and SQL

## Description
This project aims to implement a REST API and a corresponding database based on the provided documentation. The requirements include:

1. Two endpoints, one of which performs the following steps upon request:
   - Downloads Products.csv and saves it locally.
   - Reads products from the file, filters non-wire products with 24h shipping, and stores them in an SQL table.
   - Downloads Inventory.csv and saves it locally.
   - Reads inventory data, filters products with 24h shipping, and stores the data in an SQL table.
   - Downloads Prices.csv and saves it locally.
   - Reads product prices and stores them in an SQL table. 

2. The second endpoint accepts a product SKU as a parameter and returns:
   - Product details: Name, EAN, Manufacturer, Category, Image URL.
   - Inventory details: Stock quantity, Logistic unit.
   - Pricing details: Net purchase price, Shipping cost.
  
## Implementation Details:
   - Utilizes .NET 7 and C# for the backend development.
   - Employs SQL for database operations.
   - Incorporates the Dapper ORM library for efficient data access.
   - Ensures code clarity with comprehensive comments.
