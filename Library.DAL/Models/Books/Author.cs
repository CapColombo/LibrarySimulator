namespace Library.DAL.Models.Books;

public class Author
{
    public Author() { }

    public Author(string name)
    {
        Name = name;
    }

    public Guid Id { get; }

    public string Name { get; set; }

    public List<Book> Books { get; } = [];

    public void AddBooks(List<Book> books)
    {
        Books.AddRange(books);
    }
}