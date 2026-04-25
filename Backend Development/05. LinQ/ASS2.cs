using System;
using System.Collections.Generic;
using System.Linq;


// Sample Data this was created via AI to save time
 
var products = new List<Product>
{
    new Product { ProductID = 1,  ProductName = "Chai",                 UnitPrice = 18.00m,  UnitsInStock = 39,  CategoryName = "Beverages" },
    new Product { ProductID = 2,  ProductName = "Chang",                UnitPrice = 19.00m,  UnitsInStock = 17,  CategoryName = "Beverages" },
    new Product { ProductID = 3,  ProductName = "Aniseed Syrup",        UnitPrice = 10.00m,  UnitsInStock = 13,  CategoryName = "Condiments" },
    new Product { ProductID = 4,  ProductName = "Chef Anton Cajun",     UnitPrice = 22.00m,  UnitsInStock = 53,  CategoryName = "Condiments" },
    new Product { ProductID = 5,  ProductName = "Grandma Boysenberry",  UnitPrice = 25.00m,  UnitsInStock = 0,   CategoryName = "Condiments" },
    new Product { ProductID = 6,  ProductName = "Uncle Bob Pears",      UnitPrice = 30.00m,  UnitsInStock = 0,   CategoryName = "Produce" },
    new Product { ProductID = 7,  ProductName = "Northwoods Cranberry", UnitPrice = 40.00m,  UnitsInStock = 6,   CategoryName = "Condiments" },
    new Product { ProductID = 8,  ProductName = "Mishi Kobe Niku",      UnitPrice = 97.00m,  UnitsInStock = 29,  CategoryName = "Meat/Poultry" },
    new Product { ProductID = 9,  ProductName = "Ikura",                UnitPrice = 31.00m,  UnitsInStock = 31,  CategoryName = "Seafood" },
    new Product { ProductID = 10, ProductName = "Queso Cabrales",       UnitPrice = 21.00m,  UnitsInStock = 22,  CategoryName = "Dairy Products" },
    new Product { ProductID = 11, ProductName = "Queso Manchego",       UnitPrice = 38.00m,  UnitsInStock = 86,  CategoryName = "Dairy Products" },
    new Product { ProductID = 12, ProductName = "Konbu",                UnitPrice = 6.00m,   UnitsInStock = 24,  CategoryName = "Seafood" },
    new Product { ProductID = 13, ProductName = "Tofu",                 UnitPrice = 23.25m,  UnitsInStock = 35,  CategoryName = "Produce" },
    new Product { ProductID = 14, ProductName = "Genen Shouyu",         UnitPrice = 15.50m,  UnitsInStock = 39,  CategoryName = "Condiments" },
    new Product { ProductID = 15, ProductName = "Pavlova",              UnitPrice = 17.45m,  UnitsInStock = 29,  CategoryName = "Confections" },
    new Product { ProductID = 16, ProductName = "Alice Mutton",         UnitPrice = 39.00m,  UnitsInStock = 0,   CategoryName = "Meat/Poultry" },
    new Product { ProductID = 17, ProductName = "Carnarvon Tigers",     UnitPrice = 62.50m,  UnitsInStock = 42,  CategoryName = "Seafood" },
    new Product { ProductID = 18, ProductName = "Teatime Biscuits",     UnitPrice = 9.20m,   UnitsInStock = 25,  CategoryName = "Confections" },
    new Product { ProductID = 19, ProductName = "Sir Rodney Scones",    UnitPrice = 10.00m,  UnitsInStock = 3,   CategoryName = "Confections" },
    new Product { ProductID = 20, ProductName = "Gustaf Knackebrod",    UnitPrice = 16.25m,  UnitsInStock = 104, CategoryName = "Grains/Cereals" },
    new Product { ProductID = 21, ProductName = "Tunnbrod",             UnitPrice = 9.00m,   UnitsInStock = 61,  CategoryName = "Grains/Cereals" },
    new Product { ProductID = 22, ProductName = "Guarana Fantastica",   UnitPrice = 4.50m,   UnitsInStock = 20,  CategoryName = "Beverages" },
    new Product { ProductID = 23, ProductName = "NuNuCa Nuss-Nougat",   UnitPrice = 14.00m,  UnitsInStock = 76,  CategoryName = "Confections" },
    new Product { ProductID = 24, ProductName = "Gumbar Gummibarchen",  UnitPrice = 31.23m,  UnitsInStock = 15,  CategoryName = "Confections" },
    new Product { ProductID = 25, ProductName = "Schoggi Schokolade",   UnitPrice = 43.90m,  UnitsInStock = 49,  CategoryName = "Confections" },
    new Product { ProductID = 26, ProductName = "Rossle Sauerkraut",    UnitPrice = 45.60m,  UnitsInStock = 26,  CategoryName = "Produce" },
    new Product { ProductID = 27, ProductName = "Thringer Rostbratwurst",UnitPrice = 123.79m, UnitsInStock = 0,   CategoryName = "Meat/Poultry" },
    new Product { ProductID = 28, ProductName = "Nord-Ost Matjeshering",UnitPrice = 25.89m,  UnitsInStock = 10,  CategoryName = "Seafood" },
    new Product { ProductID = 29, ProductName = "Gorgonzola Telino",    UnitPrice = 12.50m,  UnitsInStock = 0,   CategoryName = "Dairy Products" },
    new Product { ProductID = 30, ProductName = "Mascarpone Fabioli",   UnitPrice = 32.00m,  UnitsInStock = 9,   CategoryName = "Dairy Products" },
};
 
var customers = new List<Customer>
{
    new Customer { CompanyName = "Alfreds Futterkiste",   Country = "Germany", Orders = new() { new Order { Total = 440 }, new Order { Total = 1863 } } },
    new Customer { CompanyName = "Ana Trujillo",          Country = "Mexico",  Orders = new() { new Order { Total = 886 } } },
    new Customer { CompanyName = "Antonio Moreno",        Country = "Mexico",  Orders = new() { new Order { Total = 330 }, new Order { Total = 2082 } } },
    new Customer { CompanyName = "Around the Horn",       Country = "UK",      Orders = new() { new Order { Total = 1379 }, new Order { Total = 454 } } },
    new Customer { CompanyName = "Berglunds Snabbkop",    Country = "Sweden",  Orders = new() { new Order { Total = 4324 } } },
    new Customer { CompanyName = "Blauer See Delikatessen",Country = "Germany",Orders = new() { new Order { Total = 539 } } },
    new Customer { CompanyName = "Blondel pere et fils",  Country = "France",  Orders = new() { new Order { Total = 1820 }, new Order { Total = 850 } } },
    new Customer { CompanyName = "Bolido Comidas",        Country = "Spain",   Orders = new() { new Order { Total = 600 } } },
    new Customer { CompanyName = "Bon app",               Country = "France",  Orders = new() { new Order { Total = 900 }, new Order { Total = 700 } } },
    new Customer { CompanyName = "Bottom Dollar Markets", Country = "Canada",  Orders = new() { new Order { Total = 2052 } } },
};

// Question 1
var top3 = products
    .OrderByDescending(p => p.UnitPrice).Take(3);
 
foreach (var p in top3)
    Console.WriteLine($"  {p.ProductName} — ${p.UnitPrice}");

// Question 2
int pageNumber = 2, pageSize = 5;
var page2 = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);
 
foreach (var p in page2)
    Console.WriteLine($"  {p.ProductName}");

// Question 3
var cheap = products.OrderBy(p => p.UnitPrice).TakeWhile(p => p.UnitPrice < 25);
 
foreach (var p in cheap)
    Console.WriteLine($"  {p.ProductName} — ${p.UnitPrice}");

// Question 4
bool allInStock = products.Where(p => p.CategoryName == "Seafood").All(p => p.UnitsInStock > 0);
 
Console.WriteLine($"  All Seafood in stock: {allInStock}");

// Question 5
int[] ids = { 3, 9, 13, 18 };
bool hasNine = ids.Contains(9);
Console.WriteLine($"  Contains 9: {hasNine}");

// Question 6
var groups6 = products.GroupBy(p => p.CategoryName);
foreach (var g in groups6)
    Console.WriteLine($"  {g.Key}: {g.Count()} products");

// Question 7
var groups7 = products
    .GroupBy(p => p.CategoryName)
    .Select(g => new
    {
        Category = g.Key,
        Names    = g.Select(p => p.ProductName).ToList()
    });
 
foreach (var g in groups7)
{
    Console.WriteLine($"\n  {g.Category}:");
    g.Names.ForEach(n => Console.WriteLine($"    - {n}"));
}

// Question 8
var bigCategories = products.GroupBy(p => p.CategoryName).Where(g => g.Count() > 3).Select(g => new { Category = g.Key, Count = g.Count() });
 
foreach (var g in bigCategories)
    Console.WriteLine($"  {g.Category}: {g.Count} products");

// Question 9
var result9 =
    from c in customers
    group c by c.Country into g
    select new
    {
        Country         = g.Key,
        Count           = g.Count(),
        TotalOrderValue = g.SelectMany(c => c.Orders).Sum(o => o.Total)
    };
 
foreach (var r in result9)
    Console.WriteLine($"  {r.Country} | {r.Count} customers | ${r.TotalOrderValue}");

// Question 10
int totalUnits = products.Sum(p => p.UnitsInStock);

// Question 11
decimal cheapest      = products.Min(p => p.UnitPrice);
Console.WriteLine($"  Cheapest:      ${cheapest}");
decimal Expensive = products.Max(p => p.UnitPrice);
Console.WriteLine($"  Expensive: ${Expensive}");

// Question 12
var categories = products.Select(p => p.CategoryName).Distinct();
 
foreach (var c in categories)
    Console.WriteLine($"  {c}");

// Question 13
int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
int[] setB = { 3, 6, 9, 12, 15, 13 };
var onlyInA = setA.Except(setB);
Console.WriteLine($"  Result: {string.Join(", ", onlyInA)}");

// Question 14
string[] list1 = { "Germany", "France", "UK", "Spain" };
string[] list2 = { "france", "SPAIN", "Italy" };
var uniqueCountries = list1
    .Where(c => !list2.Any(x => x.Equals(c, StringComparison.OrdinalIgnoreCase)));
 
foreach (var c in uniqueCountries)
    Console.WriteLine($"  {c}");

// Question 15
var productDict = products.ToDictionary(p => p.ProductID);
var product18   = productDict[18];
Console.WriteLine($"  {product18.ProductName} — ${product18.UnitPrice}");

// Question 16
var firstOver50 = products.First(p => p.UnitPrice > 50);
Console.WriteLine($"  {firstOver50.ProductName} — ${firstOver50.UnitPrice}");

// Question 17
var firstOver = products.FirstOrDefault(p => p.UnitPrice > 500);
if (firstOver is null)
    Console.WriteLine("  No product found above $500");
else
    Console.WriteLine($"  {firstOver.ProductName}");

// Question 18
var table7 = Enumerable.Range(1, 10)
    .Select(i => $"  7 x {i,2} = {7 * i}");
 
foreach (var line in table7)
    Console.WriteLine(line);

// Question 19
var evens = Enumerable.Range(1, 30).Where(n => n % 2 == 0);
Console.WriteLine($"  {string.Join(", ", evens)}");

// Question 20
var combined = products.Take(3).Select(p => p.ProductName).Concat(customers.Take(3).Select(c => c.CompanyName));
 
foreach (var name in combined)
    Console.WriteLine($"  {name}");

// Question 21
var pairs = products.Zip(customers,(p, c) => $"  {p.ProductName} sold to {c.CompanyName}");
 
foreach (var pair in pairs)
    Console.WriteLine(pair);

class Product
{
    public int ProductID { get; set; }
    public string ProductName { get; set; } = "";
    public decimal UnitPrice { get; set; }
    public int UnitsInStock { get; set; }
    public string CategoryName { get; set; } = "";
}
 
class Customer
{
    public string CompanyName { get; set; } = "";
    public string Country { get; set; } = "";
    public List<Order> Orders { get; set; } = new();
}
 
class Order
{
    public decimal Total { get; set; }
}
 