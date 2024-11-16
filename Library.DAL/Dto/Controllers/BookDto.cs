using Library.DAL.Models.Books;
using Library.DAL.Models.Enums;

namespace Library.DAL.Dto.Controllers;

public class BookDto
{
    public string Title { get; set; }

    public string Description { get; set; }

    public List<Author> Authors { get; } = [];

    public List<Genre> Genres { get; } = [];

    public DateTime PublicationDate { get; set; }

    public bool InStock { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
