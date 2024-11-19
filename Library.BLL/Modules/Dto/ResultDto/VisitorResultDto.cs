using Library.DAL.Models.Visitors;

namespace Library.BLL.Modules.Dto.ResultDto;

public class VisitorResultDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public List<RentedBook> RentedBooks { get; set; }
}
