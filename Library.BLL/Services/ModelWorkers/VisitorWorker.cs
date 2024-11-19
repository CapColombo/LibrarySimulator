using Library.DAL.Models.Statistic;
using Library.DAL.Models.Visitors;

namespace Library.BLL.Services.ModelWorkers;

public class VisitorWorker
{
    private readonly Visitor Visitor;

    public VisitorWorker(Visitor visitor)
    {
        Visitor = visitor;
    }

    public void AddBook(RentedBook book)
    {
        Visitor.RentedBooks.Add(book);
    }

    public void ReturnBook(RentedBook book)
    {
        Visitor.RentedBooks.Remove(book);
    }

    public void AddViolation(Violation violation)
    {
        Visitor.Violations.Add(violation);
    }
}
