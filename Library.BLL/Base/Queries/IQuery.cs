using MediatR;

namespace Library.BLL.Base.Queries;

public interface IQuery<out TQueryResult> : IRequest<TQueryResult> where TQueryResult : IQueryResultBase
{

}
