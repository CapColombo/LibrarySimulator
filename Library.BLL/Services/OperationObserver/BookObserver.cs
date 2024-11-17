using Library.BLL.Services.OperationObserver.Interfaces;
using Library.DAL;
using Library.DAL.Models.Books;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Statistic;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.OperationObserver;

public class BookObserver : IBookObserver
{
    private readonly ILibraryContext _context;

    public BookObserver(ILibraryContext context)
    {
        _context = context;
    }

    public async Task UpdateAsync(Operation? operation, CancellationToken token)
    {
        if (operation is null)
        {
            throw new ArgumentException(nameof(operation));
        }

        Book? book = await _context.Books.FirstOrDefaultAsync(b => b.Id == operation.BookId, token);

        if (book is null)
        {
            throw new ArgumentException(nameof(book));
        }

        book.InStock = operation.OperationType switch
        {
            OperationType.Rented => false,
            OperationType.Returned => true,
            _ => throw new NotImplementedException(),
        };

        if (operation.OperationType is OperationType.Returned)
        {
            book.PhysicalCondition = operation.PhysicalCondition;
            book.RentedBookId = null;
        }

        await _context.UpdateWithSaveAsync(book, token);
    }
}
