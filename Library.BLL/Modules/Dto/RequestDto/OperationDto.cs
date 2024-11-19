using Library.DAL.Models.Enums;

namespace LibrarySimulator.Controllers.Dto;

public class OperationDto
{
    public Guid BookId { get; set; }

    public Guid VisitorId { get; set; }

    public OperationType OperationType { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
