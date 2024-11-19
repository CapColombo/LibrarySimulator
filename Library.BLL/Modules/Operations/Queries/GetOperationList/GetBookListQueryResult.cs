using Library.BLL.Base.Queries;
using Library.BLL.Modules.Dto.ResultDto;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Operations.Queries.GetOperationList;

public class GetOperationListQueryResult : IQueryResult<OneOf<IReadOnlyList<OperationResultDto>, NotFound>>
{
    public GetOperationListQueryResult(OneOf<IReadOnlyList<OperationResultDto>, NotFound> result)
    {
        Result = result;
    }

    public OneOf<IReadOnlyList<OperationResultDto>, NotFound> Result { get; }
}
