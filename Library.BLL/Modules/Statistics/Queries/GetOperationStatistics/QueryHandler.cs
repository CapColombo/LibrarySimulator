using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Statistics.Queries.GetOperationStatistics;

public class QueryHandler : IRequestHandler<GetOperationStatisticsQuery, GetOperationStatisticsQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetOperationStatisticsQueryResult> Handle(GetOperationStatisticsQuery request, CancellationToken cancellationToken)
    {
        if (request is null)
        {
            return new GetOperationStatisticsQueryResult(new NotFound());
        }

        List<OperationStatisticsResultDto> result = await _context.Operations
            .AsNoTracking()
            .Include(o => o.Book)
            .Include(o => o.Visitor)
            .Where(o => o.Date >= request.StartDate && o.Date <= request.EndDate)
            .ProjectTo<OperationStatisticsResultDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (result is null)
        {
            return new GetOperationStatisticsQueryResult(new NotFound());
        }

        return new GetOperationStatisticsQueryResult(result);
    }
}
