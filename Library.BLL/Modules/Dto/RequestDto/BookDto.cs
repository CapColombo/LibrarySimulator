using Library.DAL.Models.Books;

namespace LibrarySimulator.Controllers.Dto;

public class BookDto
{
    public string Title { get; set; }

    public string? Description { get; set; }

    public List<Author> Authors { get; set; } = [];

    public List<Genre> Genres { get; set; } = [];

    public DateTime PublicationDate { get; set; }
}
