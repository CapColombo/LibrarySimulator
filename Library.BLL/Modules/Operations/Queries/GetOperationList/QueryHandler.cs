using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Operations.Queries.GetOperationList;

public class QueryHandler : IRequestHandler<GetOperationListQuery, GetOperationListQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetOperationListQueryResult> Handle(GetOperationListQuery request, CancellationToken cancellationToken)
    {
        if (request == null)
        {
            return new GetOperationListQueryResult(new NotFound());
        }

        List<OperationResultDto> result = await _context.Operations
            .AsNoTracking()
            .ProjectTo<OperationResultDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (result.Count == 0)
        {
            return new GetOperationListQueryResult(new NotFound());
        }

        return new GetOperationListQueryResult(result);
    }
}
