namespace Library.DAL.Models.Books;

public class Genre
{
    public Genre() { }

    public Genre(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
    }

    public Guid Id { get; set; }

    public string Title { get; set; }
}