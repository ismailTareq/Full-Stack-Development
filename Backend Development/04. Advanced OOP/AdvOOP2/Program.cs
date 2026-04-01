using System;
using System.Collections.Generic;

namespace ShopMaster
{
    // starter code
    class Product
    {
        public int    Id       { get; set; }
        public string Name     { get; set; }
        public string Category { get; set; } // "Electronics", "Clothing", "Food", "Books"
        public double Price    { get; set; }
        public int    Stock    { get; set; }
    }

    class Program
    {
        // SmartSearchProducts
        // so we can pass any filter without changing this method, we use Func<Product, bool>
        static List<Product> SearchProducts(List<Product> products, Func<Product, bool> filter)
        {
            List<Product> results = new List<Product>();
            foreach (Product p in products)
            {
                if (filter(p))
                    results.Add(p);
            }
            return results;
        }

        // CustomReportGenerator
        // PrintReports
        // using Action<Product> 3lshan lw caller decides what to print, method just loops
        static void PrintReport(List<Product> products, Action<Product> printAction)
        {
            foreach (Product p in products)
                printAction(p);
        }

        // TransformProducts
        // using Func<Product, string> takes a product, gives back a string
        static List<string> TransformProducts(List<Product> products, Func<Product, string> transform)
        {
            List<string> results = new List<string>();
            foreach (Product p in products)
                results.Add(transform(p));
            return results;
        }

        // FilterProducts
        // using Predicate<Product> basically a Func<Product, bool> but more explicit about intent
        static List<Product> FilterProducts(List<Product> products, Predicate<Product> condition)
        {
            List<Product> results = new List<Product>();
            foreach (Product p in products)
            {
                if (condition(p))
                    results.Add(p);
            }
            return results;
        }

        static void Main(string[] args)
        {
            // the product catalog shared starter data
            List<Product> catalog = new List<Product>()
            {
                new Product { Id=1,  Name="Laptop",       Category="Electronics", Price=1200, Stock=10  },
                new Product { Id=2,  Name="Phone",        Category="Electronics", Price=800,  Stock=25  },
                new Product { Id=3,  Name="T-Shirt",      Category="Clothing",    Price=30,   Stock=100 },
                new Product { Id=4,  Name="Jeans",        Category="Clothing",    Price=60,   Stock=50  },
                new Product { Id=5,  Name="Chocolate",    Category="Food",        Price=5,    Stock=200 },
                new Product { Id=6,  Name="Coffee Beans", Category="Food",        Price=15,   Stock=80  },
                new Product { Id=7,  Name="C# Book",      Category="Books",       Price=45,   Stock=30  },
                new Product { Id=8,  Name="Novel",        Category="Books",       Price=20,   Stock=60  },
                new Product { Id=9,  Name="Headphones",   Category="Electronics", Price=150,  Stock=40  },
                new Product { Id=10, Name="Jacket",       Category="Clothing",    Price=120,  Stock=15  }
            };

            // Task 1
            // 1. all electronics
            Console.WriteLine("--- Electronics ---");
            List<Product> electronics = SearchProducts(catalog, p => p.Category == "Electronics");
            foreach (Product p in electronics)
                Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})");

            // 2. cheaper than $50
            Console.WriteLine("--- Under $50 ---");
            List<Product> cheap = SearchProducts(catalog, p => p.Price < 50);
            foreach (Product p in cheap)
                Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})");

            // 3. in stock
            Console.WriteLine("--- In Stock ---");
            List<Product> inStock = SearchProducts(catalog, p => p.Stock > 0);
            foreach (Product p in inStock)
                Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})");

            // 4. clothing under $100
            Console.WriteLine("--- Clothing Under $100 ---");
            List<Product> clothingCheap = SearchProducts(catalog, p => p.Category == "Clothing" && p.Price < 100);
            foreach (Product p in clothingCheap)
                Console.WriteLine($"{p.Name} - ${p.Price} (Stock: {p.Stock})");

            // Task 3.1
            // scenario 1 - short report, Action<Product> just prints name and price
            Console.WriteLine("--- Short Report ---");
            PrintReport(catalog, p => Console.WriteLine($"{p.Name} - ${p.Price}"));

            // scenario 2 - detailed report, same method different lambda
            Console.WriteLine("--- Detailed Report ---");
            PrintReport(catalog, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));

            // Task 3.2 
            // scenario 3 - summary list, Func<Product, string> turns each product into a string
            Console.WriteLine("--- Summary List ---");
            List<string> summaries = TransformProducts(catalog, p => $"{p.Name} (${p.Price})");
            foreach (string s in summaries)
                Console.WriteLine(s);

            // scenario 4 - price labels, same Func but different output
            Console.WriteLine("--- Price Labels ---");
            List<string> labels = TransformProducts(catalog, p => $"{p.Name}: {(p.Price > 100 ? "Expensive!" : "Affordable")}");
            foreach (string s in labels)
                Console.WriteLine(s);

            // Task 3.3 
            // scenario 5 - low stock alert, Predicate<Product> finds the ones running low
            Console.WriteLine("--- Low-Stock Alert ---");
            List<Product> lowStock = FilterProducts(catalog, p => p.Stock < 20);
            foreach (Product p in lowStock)
                Console.WriteLine($"[LOW STOCK] {p.Name}: only {p.Stock} left!");
        }
    }
}