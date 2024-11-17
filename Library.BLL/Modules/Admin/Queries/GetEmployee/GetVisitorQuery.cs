using Library.BLL.Base.Queries;

namespace Library.BLL.Modules.Admin.Queries.GetEmployee;

public class GetEmployeeQuery : IQuery<GetEmployeeQueryResult>
{
    public GetEmployeeQuery(string id)
    {
        EmployeeId = id;
    }

    public string EmployeeId { get; }
}
