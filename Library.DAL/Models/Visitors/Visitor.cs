namespace Library.DAL.Models.Visitors;

public class Visitor
{
    public Visitor() { }

    public Visitor(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

    public Guid Id { get; }

    public string Name { get; set; }

    public string Email { get; set; }

    public List<RentedBook> RentedBooks { get; } = [];

    public void AddBooks(List<RentedBook> books)
    {
        RentedBooks.AddRange(books);
    }

    public void ReturnBooks(List<Guid> booksId)
    {
        RentedBooks.RemoveAll(b => booksId.Contains(b.Id));
    }
}
