﻿using Library.BLL.Base.Queries;
using Library.DAL.Dto.QueryCommandResult;
using OneOf;
using OneOf.Types;

namespace Library.BLL.Modules.Visitors.Queries.GetVisitor;

public class GetVisitorQueryResult : IQueryResult<OneOf<VisitorResultDto, NotFound>>
{
    public GetVisitorQueryResult(OneOf<VisitorResultDto, NotFound> result)
    {
        Result = result;
    }

    public OneOf<VisitorResultDto, NotFound> Result { get; }
}