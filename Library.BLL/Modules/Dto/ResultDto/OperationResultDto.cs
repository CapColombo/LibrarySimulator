using Library.DAL.Models.Enums;

namespace Library.BLL.Modules.Dto.ResultDto;

public class OperationResultDto
{
    public Guid Id { get; set; }

    public Guid BookId { get; set; }

    public string VisitorName { get; set; }

    public DateTime Date { get; set; }

    public OperationType OperationType { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
