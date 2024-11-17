using Library.BLL.Services.OperationObserver.Interfaces;
using Library.DAL;
using Library.DAL.Models.Statistic;

namespace Library.BLL.Services.OperationObserver;

public class OperationObserver : IOperationObserver
{
    private readonly ILibraryContext _context;

    public OperationObserver(ILibraryContext context)
    {
        _context = context;
    }

    public async Task UpdateAsync(Operation? operation, CancellationToken token)
    {
        if (operation is null)
        {
            throw new ArgumentException(nameof(operation));
        }

        await _context.UpdateWithSaveAsync(operation, token);
    }
}
