﻿using Library.BLL.Services.ModelWorkers;
using Library.BLL.Services.OperationObserver.Interfaces;
using Library.DAL;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Statistic;
using Library.DAL.Models.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.OperationObserver;

public class VisitorObserver : IVisitorObserver
{
    private readonly ILibraryContext _context;

    public VisitorObserver(ILibraryContext context)
    {
        _context = context;
    }

    public async Task UpdateAsync(Operation? operation, CancellationToken token)
    {
        if (operation is null)
        {
            throw new ArgumentException(nameof(operation));
        }

        Visitor? visitor = await _context.Visitors.FirstOrDefaultAsync(v => v.Id == operation.VisitorId, token);
        RentedBook? book = await _context.RentedBooks.FirstOrDefaultAsync(b => b.BookId == operation.BookId, token);

        if (visitor is null || book is null)
        {
            throw new ArgumentException($"{nameof(visitor)} or {nameof(book)}");
        }

        VisitorWorker worker = new(visitor);

        switch (operation.OperationType)
        {
            case OperationType.Rented:
                worker.AddBook(book);
                break;
            case OperationType.Returned:
                worker.ReturnBook(book);
                break;
            default:
                break;
        }
    }
}
