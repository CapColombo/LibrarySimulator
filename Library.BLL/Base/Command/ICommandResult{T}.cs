using OneOf;

namespace Library.BLL.Base.Command;

public interface ICommandResult<T> : ICommandResultBase where T : IOneOf
{
    T Result { get; }
}
