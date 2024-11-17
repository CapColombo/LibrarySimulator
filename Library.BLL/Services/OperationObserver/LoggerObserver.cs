using Library.BLL.Services.OperationObserver.Interfaces;
using Library.Common.Extensions;
using Library.DAL;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Statistic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Library.BLL.Services.OperationObserver;

public class LoggerObserver : ILoggerObserver
{
    private ILibraryContext _context;
    private ILogger<LoggerObserver> _logger;

    public LoggerObserver(ILibraryContext context, ILogger<LoggerObserver> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task UpdateAsync(Operation? operation, CancellationToken token)
    {
        if (operation is null)
        {
            throw new ArgumentException(nameof(operation));
        }

        var rentedBook = await _context.RentedBooks
            .AsNoTracking()
            .Where(b => b.BookId == operation.BookId)
            .Select(b => new
            {
                VisitorName = b.Visitor.Name,
                BookTitle = b.Book.Title,
                ReturnDate = b.ReturnDate.ToString("dd.MM.yyyy HH:mm"),
                Date = b.IssueDate.ToString("dd.MM.yyyy HH:mm"),
                Condition = b.Book.PhysicalCondition.GetDescription(),
            })
            .FirstOrDefaultAsync(token);

        if (operation.OperationType is OperationType.Rented)
        {
            string message =
                $"{rentedBook.Date}: посетитель {rentedBook.VisitorName} " +
                $"взял книгу {rentedBook.BookTitle}. " +
                $"Срок аренды до {rentedBook.ReturnDate}. " +
                $"Состояние книги: '{rentedBook.Condition}'";

            _logger.LogInformation(message);

            return;
        }

        if (operation.OperationType is OperationType.Rented)
        {
            string message =
                $"{rentedBook.Date}: посетитель {rentedBook.VisitorName} " +
                $"вернул книгу {rentedBook.BookTitle}. " +
                $"Состояние книги: '{rentedBook.Condition}'";

            _logger.LogInformation(message);

            return;
        }
    }
}
