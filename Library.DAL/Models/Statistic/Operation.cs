using Library.DAL.Models.Books;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Visitors;

namespace Library.DAL.Models.Statistic;

public class Operation
{
    public Operation() { }

    public Operation(Guid bookId, Guid visitorId, OperationType operationType, PhysicalCondition physicalCondition, int? period = null)
    {
        Id = Guid.NewGuid();
        BookId = bookId;
        VisitorId = visitorId;
        Date = DateTime.Now;
        OperationType = operationType;
        PhysicalCondition = physicalCondition;
        RentalPeriod = operationType is OperationType.Rented ? period : null;
    }

    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public Book Book { get; set; }

    public Guid VisitorId { get; set; }

    public Visitor Visitor { get; set; }

    public DateTime Date { get; set; }

    public OperationType OperationType { get; set; }

    public int? RentalPeriod { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
