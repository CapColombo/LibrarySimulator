using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.BLL.Modules.Visitors.Queries.GetVisitor;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Visitors.Queries.GetVisitorList;

public class QueryHandler : IRequestHandler<GetVisitorListQuery, GetVisitorListQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetVisitorListQueryResult> Handle(GetVisitorListQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return new GetVisitorListQueryResult(new NotFound());
        }

        List<VisitorResultDto> result = await _context.Visitors
            .AsNoTracking()
            .Include(v => v.RentedBooks)
            .ProjectTo<VisitorResultDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (result.Count == 0)
        {
            return new GetVisitorListQueryResult(new NotFound());
        }

        return new GetVisitorListQueryResult(result);
    }
}
