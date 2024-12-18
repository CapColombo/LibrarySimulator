﻿using Library.BLL.Base.Queries;
using Library.BLL.Modules.Dto.ResultDto;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Visitors.Queries.GetVisitorList;

public class GetVisitorListQueryResult : IQueryResult<OneOf<IReadOnlyList<VisitorResultDto>, NotFound>>
{
    public GetVisitorListQueryResult(OneOf<IReadOnlyList<VisitorResultDto>, NotFound> result)
    {
        Result = result;
    }

    public OneOf<IReadOnlyList<VisitorResultDto>, NotFound> Result { get; }
}
