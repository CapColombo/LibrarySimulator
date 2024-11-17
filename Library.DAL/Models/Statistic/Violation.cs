using Library.DAL.Models.Books;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Visitors;

namespace Library.DAL.Models.Statistic;

public class Violation
{
    public Violation() { }

    public Violation(
        DateTime date,
        Guid visitorId,
        Guid bookId,
        ViolationType violationType,
        PhysicalCondition originalCondition,
        PhysicalCondition finalCondition,
        int? overdueDays = null)
    {
        Id = Guid.NewGuid();
        Date = date;
        VisitorId = visitorId;
        BookId = bookId;
        ViolationType = violationType;
        OriginalCondition = originalCondition;
        FinalCondition = finalCondition;
        OverdueDays = overdueDays;
    }

    public Guid Id { get; }

    public DateTime Date { get; }

    public Guid VisitorId { get; set; }

    public Visitor Visitor { get; set; }

    public Guid BookId { get; set; }

    public Book Book { get; set; }

    public ViolationType ViolationType { get; }

    public PhysicalCondition OriginalCondition { get; }

    public PhysicalCondition FinalCondition { get; }

    public int? OverdueDays { get; set; }
}
