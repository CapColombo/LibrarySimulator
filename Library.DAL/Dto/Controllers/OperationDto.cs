using Library.DAL.Models.Enums;

namespace Library.DAL.Dto.Controllers;

public class OperationDto
{
    public Guid BookId { get; set; }

    public Guid VisitorId { get; set; }

    public OperationType OperationType { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
