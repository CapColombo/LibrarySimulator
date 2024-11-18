using Library.DAL.Models.Enums;
using Library.DAL.Models.Visitors;

namespace Library.DAL.Models.Books;

public class Book
{
    public Book() { }

    public Book(string title, string description, List<Author> authors, List<Genre> genres, DateTime publicationDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Authors = authors;
        Genres = genres;
        PublicationDate = publicationDate;
        InStock = true;
        PhysicalCondition = PhysicalCondition.New;
    }

    public Guid Id { get; set; }

    public Guid? RentedBookId { get; set; }

    public RentedBook RentedBook { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<Author> Authors { get; set; } = [];

    public List<Genre> Genres { get; set; } = [];

    public DateTime PublicationDate { get; set; }

    public bool InStock { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
