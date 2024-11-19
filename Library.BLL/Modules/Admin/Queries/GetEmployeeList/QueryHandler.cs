using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.BLL.Modules.Admin.Commands.AddEmployee;
using Library.BLL.Modules.Dto.ResultDto;
using Library.DAL;
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
        if (request == null)
        {
            return new GetEmployeeListQueryResult(new NotFound());
        }

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
