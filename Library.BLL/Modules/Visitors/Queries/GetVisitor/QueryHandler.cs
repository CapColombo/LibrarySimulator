using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Visitors.Queries.GetVisitor;

public class QueryHandler : IRequestHandler<GetVisitorQuery, GetVisitorQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetVisitorQueryResult> Handle(GetVisitorQuery request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.VisitorId, out Guid visitorId))
        {
            return new GetVisitorQueryResult(new NotFound());
        }

        VisitorResultDto? result = await _context.Visitors
            .AsNoTracking()
            .Include(v => v.RentedBooks)
            .ProjectTo<VisitorResultDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(v => v.Id == visitorId, cancellationToken);

        if (result is null)
        {
            return new GetVisitorQueryResult(new NotFound());
        }

        return new GetVisitorQueryResult(result);
    }
}
