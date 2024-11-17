using Library.DAL;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Statistic;
using Library.DAL.Models.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.ExpiredViolationWorker;

public class ExpiredViolationWorker : IExpiredViolationWorker
{
    private readonly ILibraryContext _context;

    public ExpiredViolationWorker(ILibraryContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Ищет просроченные книги и обновляет просрок в нарушениях или создает новое нарушение
    /// </summary>
    public async Task FindExpiredViolationsAsync(CancellationToken token)
    {
        List<RentedBook> expiredBooks = await _context.RentedBooks
            .AsNoTracking()
            .Where(b => (b.ReturnDate - b.IssueDate).Days < 0)
            .ToListAsync(token);

        IEnumerable<Guid> expiredBookIds = expiredBooks.Select(b => b.BookId);
        IEnumerable<Guid> visitorsIds = expiredBooks.Select(b => b.VisitorId);

        var bookConditions = await _context.Books
            .AsNoTracking()
            .Where(b => expiredBookIds.Contains(b.Id))
            .Select(b => new { RentedBookId = b.RentedBookId, Condition = b.PhysicalCondition })
            .ToListAsync(token);

        List<Visitor> visitors = await _context.Visitors
            .Where(v => visitorsIds.Contains(v.Id))
            .ToListAsync(token);

        if (expiredBooks.Count == 0)
        {
            return;
        }

        foreach (var rentedBook in expiredBooks)
        {
            int period = (rentedBook.ReturnDate - rentedBook.IssueDate).Days;

            var existViolation = await _context.Violations
                .Where(v => v.VisitorId == rentedBook.VisitorId && v.BookId == rentedBook.BookId)
                .FirstOrDefaultAsync(token);

            if (existViolation is not null)
            {
                existViolation.OverdueDays = period;
                continue;
            }

            PhysicalCondition condition = bookConditions.Where(b => b.RentedBookId == rentedBook.Id).Select(b => b.Condition).First();

            Violation violation = new(DateTime.Now, rentedBook.VisitorId, rentedBook.BookId,
                                ViolationType.DamagedBook, condition, condition, period);

            Visitor visitor = visitors.First(v => v.Id == rentedBook.VisitorId);

            _context.Add(violation);
            visitor.AddViolation(violation);
        }

        await _context.SaveChangesAsync(token);
    }
}
