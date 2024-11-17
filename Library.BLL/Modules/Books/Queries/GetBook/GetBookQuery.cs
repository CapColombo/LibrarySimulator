using Library.BLL.Base.Queries;

namespace Library.BLL.Modules.Books.Queries.GetBook;

public class GetBookQuery : IQuery<GetBookQueryResult>
{
    public GetBookQuery(string id)
    {
        BookId = id;
    }

    public string BookId { get; }
}
