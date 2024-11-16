namespace Library.DAL.Models.Visitors;

public class RentedBook
{
    public RentedBook() { }

    public RentedBook(Guid bookId, Guid visitorId)
    {
        Id = Guid.NewGuid();
        BookId = bookId;
        VisitorId = visitorId;
        IssueDate = DateTime.Now;
    }

    public Guid Id { get; }

    public Guid BookId { get; }

    public Guid VisitorId { get; set; }

    public DateTime IssueDate { get; }

    public DateTime? ReturnDate { get; set; } = null;
}