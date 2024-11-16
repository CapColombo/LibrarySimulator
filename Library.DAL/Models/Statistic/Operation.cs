using Library.DAL.Models.Enums;

namespace Library.DAL.Models.Statistic;

public class Operation
{
    public Operation() { }

    public Operation(Guid bookId, Guid visitorId, OperationType operationType, PhysicalCondition physicalCondition)
    {
        Id = Guid.NewGuid();
        BookId = bookId;
        VisitorId = visitorId;
        Date = DateTime.Now;
        OperationType = operationType;
        PhysicalCondition = physicalCondition;
    }

    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public Guid VisitorId { get; set; }

    public DateTime Date { get; set; }

    public OperationType OperationType { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
