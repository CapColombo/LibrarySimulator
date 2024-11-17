using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Admin.Queries.GetEmployeeList;

public class QueryHandler : IRequestHandler<GetEmployeeListQuery, GetEmployeeListQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetEmployeeListQueryResult> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
    {
        List<EmployeeResultDto> result = await _context.Employees
            .AsNoTracking()
            .Include(v => v.Role)
            .ProjectTo<EmployeeResultDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        if (result.Count == 0)
        {
            return new GetEmployeeListQueryResult(new NotFound());
        }

        return new GetEmployeeListQueryResult(result);
    }
}
