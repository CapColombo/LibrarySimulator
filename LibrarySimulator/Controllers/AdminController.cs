using Library.BLL.Modules.Admin.Commands.AddEmployee;
using Library.BLL.Modules.Admin.Commands.ChangeEmployee;
using Library.BLL.Modules.Admin.Commands.DeleteEmployee;
using Library.BLL.Modules.Admin.Queries.GetEmployee;
using Library.BLL.Modules.Admin.Queries.GetEmployeeList;
using Library.DAL.Dto.Controllers;
using Library.DAL.Models.Employees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Authorize(Roles = $"{RolesNames.Administrator}")]
[Route("api/admin")]
[ApiController]
public class AdminController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetEmployeesList([FromServices] IMediator mediator)
    {
        GetEmployeeListQueryResult queryResult = await mediator.Send(new GetEmployeeListQuery());

        return queryResult.Result.Match<IActionResult>(
            data => Json(data),
            error => BadRequest());
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployee([FromServices] IMediator mediator, string id)
    {
        GetEmployeeQueryResult queryResult = await mediator.Send(new GetEmployeeQuery(id));

        return queryResult.Result.Match<IActionResult>(
            data => Json(data),
            error => BadRequest());
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee([FromServices] IMediator mediator, EmployeeDto employeeDto)
    {
        AddEmployeeCommandResult commandResult = await mediator.Send(new AddEmployeeCommand(employeeDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpPut]
    public async Task<IActionResult> ChangeEmployee([FromServices] IMediator mediator, string id, EmployeeDto employeeDto)
    {
        ChangeEmployeeCommandResult commandResult = await mediator.Send(new ChangeEmployeeCommand(id, employeeDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee([FromServices] IMediator mediator, string id)
    {
        DeleteEmployeeCommandResult commandResult = await mediator.Send(new DeleteEmployeeCommand(id));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }
}
