using Library.BLL.Modules.Admin.Commands.AddEmployee;
using Library.BLL.Modules.Admin.Commands.ChangeEmployee;
using Library.BLL.Modules.Admin.Commands.DeleteEmployee;
using Library.BLL.Modules.Admin.Queries.GetEmployee;
using Library.BLL.Modules.Admin.Queries.GetEmployeeList;
using Library.DAL.Constants;
using Library.DAL.Dto.Controllers;
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
    [Route("list")]
    public async Task<IActionResult> GetEmployeeListAsync([FromServices] IMediator mediator)
    {
        GetEmployeeListQueryResult queryResult = await mediator.Send(new GetEmployeeListQuery());

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            error => NotFound());
    }

    [HttpGet]    
    public async Task<IActionResult> GetEmployeeAsync([FromServices] IMediator mediator, string id)
    {
        GetEmployeeQueryResult queryResult = await mediator.Send(new GetEmployeeQuery(id));

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            error => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployeeAsync([FromServices] IMediator mediator, EmployeeDto employeeDto)
    {
        AddEmployeeCommandResult commandResult = await mediator.Send(new AddEmployeeCommand(employeeDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpPut]
    public async Task<IActionResult> ChangeEmployeeAsync([FromServices] IMediator mediator, string id, EmployeeDto employeeDto)
    {
        ChangeEmployeeCommandResult commandResult = await mediator.Send(new ChangeEmployeeCommand(id, employeeDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployeeAsync([FromServices] IMediator mediator, string id)
    {
        DeleteEmployeeCommandResult commandResult = await mediator.Send(new DeleteEmployeeCommand(id));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }
}
