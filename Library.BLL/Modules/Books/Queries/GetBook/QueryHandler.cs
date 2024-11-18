using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Queries.GetBook;

public class QueryHandler : IRequestHandler<GetBookQuery, GetBookQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetBookQueryResult> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.BookId, out Guid bookId))
        {
            return new GetBookQueryResult(new NotFound());
        }

        BookResultDto? result = await _context.Books
            .AsNoTracking()
            .Include(b => b.Genres)
            .Include(b => b.Authors)
            .ProjectTo<BookResultDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(v => v.Id == bookId, cancellationToken);

        if (result is null)
        {
            return new GetBookQueryResult(new NotFound());
        }

        return new GetBookQueryResult(result);
    }
}
