using Library.DAL.Models.Books;
using Library.DAL.Models.Enums;

namespace Library.DAL.Dto.QueryCommandResult;

public class BookResultDto
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<Author> Authors { get; set; }

    public List<Genre> Genres { get; set; }

    public DateTime PublicationDate { get; set; }

    public bool InStock { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
