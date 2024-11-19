using Library.BLL.Base.Queries;
using Library.BLL.Modules.Dto.ResultDto;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Admin.Queries.GetEmployee;

public class GetEmployeeQueryResult : IQueryResult<OneOf<EmployeeResultDto, NotFound>>
{
    public GetEmployeeQueryResult(OneOf<EmployeeResultDto, NotFound> result)
    {
        Result = result;
    }

    public OneOf<EmployeeResultDto, NotFound> Result { get; }
}
