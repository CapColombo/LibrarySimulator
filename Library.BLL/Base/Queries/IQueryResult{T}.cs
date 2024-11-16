using OneOf;

namespace Library.BLL.Base.Queries;

public interface IQueryResult<T> : IQueryResultBase where T : IOneOf
{
    T Result { get; }
}
