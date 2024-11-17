using AutoMapper;
using AutoMapper.QueryableExtensions;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf.Types;

namespace Library.BLL.Modules.Admin.Queries.GetEmployee;

public class QueryHandler : IRequestHandler<GetEmployeeQuery, GetEmployeeQueryResult>
{
    private readonly ILibraryContext _context;
    private readonly IMapper _mapper;

    public QueryHandler(ILibraryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetEmployeeQueryResult> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
    {
        if (request is null || !Guid.TryParse(request.EmployeeId, out Guid employeeId))
        {
            return new GetEmployeeQueryResult(new NotFound());
        }

        EmployeeResultDto? result = await _context.Employees
            .AsNoTracking()
            .Include(v => v.Role)
            .ProjectTo<EmployeeResultDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(v => v.Id == employeeId, cancellationToken);

        if (result is null)
        {
            return new GetEmployeeQueryResult(new NotFound());
        }

        return new GetEmployeeQueryResult(result);
    }
}
