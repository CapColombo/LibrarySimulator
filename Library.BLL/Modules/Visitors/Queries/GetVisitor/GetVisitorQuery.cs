using Library.BLL.Base.Queries;

namespace Library.BLL.Modules.Visitors.Queries.GetVisitor;

public class GetVisitorQuery : IQuery<GetVisitorQueryResult>
{
    public GetVisitorQuery(string id)
    {
        VisitorId = id;
    }

    public string VisitorId { get; }
}
