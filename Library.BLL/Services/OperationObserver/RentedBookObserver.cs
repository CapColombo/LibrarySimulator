using Library.BLL.Services.OperationObserver.Interfaces;
using Library.DAL;
using Library.DAL.Models.Books;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Statistic;
using Library.DAL.Models.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.OperationObserver;

public class RentedBookObserver : IRentedBookObserver
{
    private readonly ILibraryContext _context;

    public RentedBookObserver(ILibraryContext context)
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

        if (operation.OperationType is OperationType.Rented)
        {
            RentedBook rentedBook = new(book.Id, operation.VisitorId, operation.RentalPeriod);
            book.RentedBookId = rentedBook.Id;

            _context.RentedBooks.Add(rentedBook);
        }
        else if (operation.OperationType is OperationType.Returned)
        {
            RentedBook? rentedBook = await _context.RentedBooks.FirstOrDefaultAsync(b => b.Id == book.RentedBookId, token);
            if (rentedBook is null)
            {
                throw new ArgumentException(nameof(rentedBook));
            }

            rentedBook.HasReturned = true;
            book.RentedBookId = null;
        }

        await _context.SaveChangesAsync(token);
    }
}
