using Library.BLL.Base.Queries;
using Library.BLL.Modules.Dto.ResultDto;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Admin.Queries.GetEmployeeList;

public class GetEmployeeListQueryResult : IQueryResult<OneOf<IReadOnlyList<EmployeeResultDto>, NotFound>>
{
    public GetEmployeeListQueryResult(OneOf<IReadOnlyList<EmployeeResultDto>, NotFound> result)
    {
        Result = result;
    }

    public OneOf<IReadOnlyList<EmployeeResultDto>, NotFound> Result { get; }
}
