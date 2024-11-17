using Library.BLL.Base.Queries;

namespace Library.BLL.Modules.Statistics.Queries.GetOperationStatistics;

public class GetOperationStatisticsQuery : IQuery<GetOperationStatisticsQueryResult>
{
    public GetOperationStatisticsQuery(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateTime StartDate { get; }
    public DateTime EndDate { get; }
}
