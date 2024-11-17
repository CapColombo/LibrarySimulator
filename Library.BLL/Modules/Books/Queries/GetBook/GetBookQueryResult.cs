using Library.BLL.Base.Queries;
using Library.DAL.Dto.QueryCommandResult;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Queries.GetBook;

public class GetBookQueryResult : IQueryResult<OneOf<BookResultDto, NotFound>>
{
    public GetBookQueryResult(OneOf<BookResultDto, NotFound> result)
    {
        Result = result;
    }

    public OneOf<BookResultDto, NotFound> Result { get; }
}
