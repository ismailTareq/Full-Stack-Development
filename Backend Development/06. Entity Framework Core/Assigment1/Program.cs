using ReadMoreBooks;
using ReadMoreBooks.Models;

// kida 7fta7 connection to the DB
using var context = new BookstoreContext();

Console.WriteLine("Connected to: ReadMoreBooksDb");
Console.WriteLine($"Database exists: {context.Database.CanConnect()}");

var category = new Category
{
    Name        = "Fiction",
    Description = "Novels and short stories",
    IsActive    = true
};

var author = new Author
{
    FirstName   = "ismail",
    LastName    = "tarek",
    Email       = "Ismailtarek12@gmail.com",
    Biography   = "English novelist and essayist.",
    DateOfBirth = new DateTime(2000, 5, 12)
};

var book = new Book
{
    Title           = "Scared of woman",
    ISBN            = "978-0-14-103614-4",
    Price           = 150m,
    NumberOfPages   = 328,
    PublicationYear = 2023,
    IsInStock       = true
};

context.Categories.Add(category);
context.Authors.Add(author);
context.Books.Add(book);
context.SaveChanges();

Console.WriteLine($"\nBooks in database    : {context.Books.Count()}");
Console.WriteLine($"Authors in database  : {context.Authors.Count()}");
Console.WriteLine($"Categories in database: {context.Categories.Count()}");

Console.WriteLine("\nDatabase created and seeded successfully!");
