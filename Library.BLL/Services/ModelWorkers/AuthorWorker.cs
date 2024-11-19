using Library.DAL.Models.Books;

namespace Library.BLL.Services.ModelWorkers;

public class AuthorWorker
{
    private readonly Author Author;

    public AuthorWorker(Author author)
    {
        Author = author;
    }

    public void AddBooks(List<Book> books)
    {
        Author.Books.AddRange(books);
    }
}
