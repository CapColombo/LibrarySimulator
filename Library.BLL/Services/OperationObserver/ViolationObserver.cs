﻿using Library.BLL.Services.ModelWorkers;
using Library.BLL.Services.OperationObserver.Interfaces;
using Library.Common.Extensions;
using Library.DAL;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Statistic;
using Library.DAL.Models.Visitors;
using Microsoft.EntityFrameworkCore;

namespace Library.BLL.Services.OperationObserver;

public class ViolationObserver : IViolationObserver
{
    private readonly ILibraryContext _context;

    public ViolationObserver(ILibraryContext context)
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

        if (visitor is null)
        {
            throw new ArgumentException(nameof(visitor));
        }

        VisitorWorker worker = new(visitor);

        PhysicalCondition originalCondition = await _context.Books
            .AsNoTracking()
            .Where(b => b.Id == operation.BookId)
            .Select(b => b.PhysicalCondition)
            .FirstOrDefaultAsync(token);

        var periodDates = await _context.RentedBooks
            .AsNoTracking()
            .Where(b => b.BookId == operation.BookId)
            .Select(b => new { b.IssueDate, b.ReturnDate })
            .FirstOrDefaultAsync(token);

        int period = (periodDates.ReturnDate - periodDates.IssueDate).Days;

        bool hasDamaged = originalCondition.ToInt() < operation.PhysicalCondition.ToInt();
        bool hasExpired = period < 0;

        Violation? damageViolation = null;
        Violation? expiredViolation = null;

        if (hasDamaged)
        {
            damageViolation = new(DateTime.Now, operation.VisitorId, operation.BookId,
                ViolationType.DamagedBook, originalCondition, operation.PhysicalCondition);

            await _context.AddAsync(damageViolation, token);

            worker.AddViolation(damageViolation);
        }

        if (hasExpired)
        {
            expiredViolation = await _context.Violations
                .Where(v => v.VisitorId == operation.VisitorId && v.BookId == operation.BookId)
                .FirstOrDefaultAsync(token);

            if (expiredViolation == null)
            {
                expiredViolation = new(DateTime.Now, operation.VisitorId, operation.BookId,
                                ViolationType.DamagedBook, originalCondition, operation.PhysicalCondition, period);

                await _context.AddAsync(expiredViolation, token);
                worker.AddViolation(expiredViolation);
            }
            else
            {
                expiredViolation.OverdueDays = period;
            }
        }

        await _context.SaveChangesAsync(token);
    }
}
