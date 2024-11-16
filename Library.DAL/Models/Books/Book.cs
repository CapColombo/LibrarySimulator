using Library.DAL.Models.Enums;

namespace Library.DAL.Models.Books;

public class Book
{
    public Book() { }

    public Book(string title, string description, List<Author> authors, DateTime publicationDate)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Authors = authors;
        PublicationDate = publicationDate;
        InStock = true;
        PhysicalCondition = PhysicalCondition.New;
    }

    public Guid Id { get; }

    public string Title { get; set; }

    public string Description { get; set; }

    public List<Author> Authors { get; } = [];

    public List<Genre> Genres { get; } = [];

    public DateTime PublicationDate { get; set; }

    public bool InStock { get; set; }

    public PhysicalCondition PhysicalCondition { get; set; }
}
