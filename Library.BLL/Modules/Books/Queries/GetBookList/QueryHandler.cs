using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Queries.GetBookList;

public class QueryHandler : IRequestHandler<GetBookListQuery, GetBookListQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetBookListQueryResult> Handle(GetBookListQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return new GetBookListQueryResult(new NotFound());
        }

        List<BookResultDto> result = await _context.Books
            .AsNoTracking()
            .Include(b => b.Genres)
            .Include(b => b.Authors)
            .ProjectTo<BookResultDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (result.Count == 0)
        {
            return new GetBookListQueryResult(new NotFound());
        }

        return new GetBookListQueryResult(result);
    }
}
