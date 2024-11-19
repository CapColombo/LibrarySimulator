using Library.BLL.Base.Queries;
using Library.BLL.Modules.Dto.ResultDto;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Statistics.Queries.GetOperationStatistics;

public class GetOperationStatisticsQueryResult : IQueryResult<OneOf<List<OperationStatisticsResultDto>, NotFound>>
{
    public GetOperationStatisticsQueryResult(OneOf<List<OperationStatisticsResultDto>, NotFound> result)
    {
        Result = result;
    }

    public OneOf<List<OperationStatisticsResultDto>, NotFound> Result { get; }
}
