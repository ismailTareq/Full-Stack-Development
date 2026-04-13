using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public string  ProductName  { get; set; }
    public string  Category     { get; set; }
    public decimal Price        { get; set; }
    public int     Stock        { get; set; }
}
class Order
{
    public string   CustomerID  { get; set; }
    public DateTime OrderDate   { get; set; }
}

class Customer
{
    public string CustomerID    { get; set; }
    public List<Order> Orders   { get; set; }
}
class Program
{
    static void Main()
    {
        // 5lit AI y3mili el List deh bs
        static List<Product> ProductList = new List<Product>
        {
            new Product { ProductName = "Chai",               Category = "Beverages",   Price = 18.00m, Stock = 39 },
            new Product { ProductName = "Chang",              Category = "Beverages",   Price = 19.00m, Stock = 17 },
            new Product { ProductName = "Guaraná Fantástica", Category = "Beverages",   Price = 4.50m,  Stock = 20 },
            new Product { ProductName = "Aniseed Syrup",      Category = "Condiments",  Price = 10.00m, Stock = 13 },
            new Product { ProductName = "Chef Anton's",       Category = "Condiments",  Price = 22.00m, Stock = 0  },
            new Product { ProductName = "Genen Shouyu",       Category = "Condiments",  Price = 15.50m, Stock = 39 },
            new Product { ProductName = "Ikura",              Category = "Seafood",     Price = 31.00m, Stock = 31 },
            new Product { ProductName = "Konbu",              Category = "Seafood",     Price = 6.00m,  Stock = 24 },
            new Product { ProductName = "Carnarvon Tigers",   Category = "Seafood",     Price = 62.50m, Stock = 42 },
            new Product { ProductName = "Nord-Ost Matjes",    Category = "Seafood",     Price = 25.89m, Stock = 10 },
            new Product { ProductName = "Tofu",               Category = "Produce",     Price = 23.25m, Stock = 0  },
            new Product { ProductName = "Rössle Sauerkraut",  Category = "Produce",     Price = 45.60m, Stock = 26 },
            new Product { ProductName = "Teatime Biscuits",   Category = "Confections", Price = 9.20m,  Stock = 25 },
            new Product { ProductName = "Sir Rodney's Jam",   Category = "Confections", Price = 81.00m, Stock = 0  },
        };

        // Question 1
        var seafoodProducts = ProductList.Where(p => p.Category == "Seafood");
        foreach (var p in seafoodProducts)
            Console.WriteLine($"Name: {p.ProductName} | Price: ${p.Price}");

        // Question 2
        var productNames = ProductList.Select(p => p.ProductName);
        foreach (var name in productNames)
            Console.WriteLine(name);

        // Question 3
        var sortedProducts = ProductList.OrderBy(p => p.Price);
        foreach (var p in sortedProducts)
            Console.WriteLine($"Name: {p.ProductName} | Price: ${p.Price}");

        // Question 4
        var midRangeProducts = ProductList.Where(p => p.Price >= 10 && p.Price <= 30);
        foreach (var p in midRangeProducts)
            Console.WriteLine($"Name: {p.ProductName} | Price: ${p.Price}");

        // Question 5
        var inStockCondiments = ProductList.Where(p => p.Category == "Condiments" && p.Stock > 0);
        foreach (var p in inStockCondiments)
            Console.WriteLine($"Name: {p.ProductName} | Stock: {p.Stock}");

        
    }
}