namespace ReadMoreBooks.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int NumberOfPages { get; set; }
    public int PublicationYear { get; set; }
    public bool IsInStock { get; set; }
}
