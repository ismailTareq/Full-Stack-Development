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

    static List<Customer> CustomerList = new List<Customer>
    {
        new Customer { CustomerID = "ALFKI", Orders = new List<Order> {
            new Order { CustomerID = "ALFKI", OrderDate = new DateTime(1997, 8, 25) },
            new Order { CustomerID = "ALFKI", OrderDate = new DateTime(1998, 1, 15) }}},
        new Customer { CustomerID = "BONAP", Orders = new List<Order> {
            new Order { CustomerID = "BONAP", OrderDate = new DateTime(1996, 10, 16) },
            new Order { CustomerID = "BONAP", OrderDate = new DateTime(1997,  3, 25) }}},
        new Customer { CustomerID = "CENTC", Orders = new List<Order> {
            new Order { CustomerID = "CENTC", OrderDate = new DateTime(1996, 7, 5) }}},
    };
    
    static string[] Arr = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
    static string[] digits = { "five", "six", "seven", "eight", "nine" };

    static void Main()
    {
        Console.WriteLine("=== Question 1 ===");
        var seafoodProducts = ProductList.Where(p => p.Category == "Seafood");
        foreach (var p in seafoodProducts)
            Console.WriteLine($"Name: {p.ProductName} | Price: ${p.Price}");

        Console.WriteLine("\n=== Question 2 ===");
        var productNames = ProductList.Select(p => p.ProductName);
        foreach (var name in productNames)
            Console.WriteLine(name);

        Console.WriteLine("\n=== Question 3 ===");
        var sortedProducts = ProductList.OrderBy(p => p.Price);
        foreach (var p in sortedProducts)
            Console.WriteLine($"Name: {p.ProductName} | Price: ${p.Price}");

        Console.WriteLine("\n=== Question 4 ===");
        var midRangeProducts = ProductList.Where(p => p.Price >= 10 && p.Price <= 30);
        foreach (var p in midRangeProducts)
            Console.WriteLine($"Name: {p.ProductName} | Price: ${p.Price}");

        Console.WriteLine("\n=== Question 5 ===");
        var inStockCondiments = ProductList.Where(p => p.Category == "Condiments" && p.Stock > 0);
        foreach (var p in inStockCondiments)
            Console.WriteLine($"Name: {p.ProductName} | Stock: {p.Stock}");

        Console.WriteLine("\n=== Question 6 ===");
        var result6 = ProductList.Select(p => new
        {
            Name        = p.ProductName,
            Price       = p.Price,
            StockStatus = p.Stock > 0 ? "Available" : "Out of Stock"
        });
        foreach (var item in result6)
            Console.WriteLine($"Name: {item.Name} | Price: ${item.Price} | Status: {item.StockStatus}");
        
        Console.WriteLine("\n=== Question 7 ===");
        var numbered = ProductList.Select((p, index) => $"{index + 1}. {p.ProductName}");
        Console.WriteLine(string.Join(", ", numbered));

        Console.WriteLine("\n=== Question 8 ===");
        var sorted8 = ProductList.OrderBy(p => p.Category).ThenByDescending(p => p.Price);
        foreach (var p in sorted8)
            Console.WriteLine($"[{p.Category}] {p.ProductName} - ${p.Price}");

        Console.WriteLine("\n=== Question 9 ===");
        var beverages = ProductList.Where(p => p.Category == "Beverages").OrderByDescending(p => p.Stock);
        foreach (var p in beverages)
            Console.WriteLine($"Name: {p.ProductName} | Stock: {p.Stock}");

        Console.WriteLine("\n=== Question 10 ===");
        var recentOrders =
            from customer in CustomerList
            from order    in customer.Orders
            where order.OrderDate.Year >= 1997
            select new
            {
                customer.CustomerID,
                order.OrderDate
            };
        foreach (var o in recentOrders)
            Console.WriteLine($"CustomerID: {o.CustomerID} | OrderDate: {o.OrderDate:yyyy-MM-dd}");

        Console.WriteLine("\n=== Question 11 ===");
        var result11 = ProductList.Select((p, i) => new
        {
            Position    = i + 1, 
            p.ProductName
        });
        foreach (var x in result11)
            Console.WriteLine($"Position: {x.Position} | Product: {x.ProductName}");

        Console.WriteLine("\n=== Question 12 ===");
        var sorted12 = Arr.OrderBy(w => w.Length).ThenBy(w => w, StringComparer.OrdinalIgnoreCase);
        foreach (var word in sorted12)
            Console.WriteLine($"{word}  (length: {word.Length})");

        Console.WriteLine("\n=== Question 13 ===");
        var result13 = digits.Where(w => w.Length >= 2 && char.ToLower(w[1]) == 'i').Reverse();
        foreach (var w in result13)
            Console.WriteLine(w);
    }
}