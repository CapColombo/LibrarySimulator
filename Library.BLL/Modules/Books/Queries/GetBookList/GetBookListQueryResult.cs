using Library.BLL.Base.Queries;
using Library.BLL.Modules.Dto.ResultDto;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Books.Queries.GetBookList;

public class GetBookListQueryResult : IQueryResult<OneOf<IReadOnlyList<BookResultDto>, NotFound>>
{
    public GetBookListQueryResult(OneOf<IReadOnlyList<BookResultDto>, NotFound> result)
    {
        Result = result;
    }

    public OneOf<IReadOnlyList<BookResultDto>, NotFound> Result { get; }
}
