using Library.DAL.Models.Books;

namespace Library.DAL.Models.Visitors;

public class RentedBook
{
    public RentedBook() { }

    public RentedBook(Guid bookId, Guid visitorId, int period)
    {
        Id = Guid.NewGuid();
        BookId = bookId;
        VisitorId = visitorId;
        IssueDate = DateTime.Now;
        ReturnDate = IssueDate.AddDays(period);
        HasReturned = false;
    }

    public Guid Id { get; }

    public Guid BookId { get; }

    public Book Book { get; set; }

    public Guid VisitorId { get; }

    public Visitor Visitor { get; set; }

    public DateTime IssueDate { get; }

    public DateTime ReturnDate { get; }

    public bool HasReturned { get; set; }
}