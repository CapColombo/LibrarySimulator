using Library.DAL.Models.Enums;

namespace Library.DAL.Dto.QueryCommandResult;

public class OperationStatisticsResultDto
{
    public DateTime Date { get; set; }

    public string VisitorName { get; set; }

    public string BookTitle { get; set; }

    public OperationType OperationType { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
