using Library.BLL.Modules.Operations.Commands.AddOperation;
using Library.BLL.Modules.Operations.Queries.GetOperationList;
using Library.DAL.Dto.Controllers;
using Library.DAL.Models.Employees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Authorize(Roles = $"{RolesNames.Administrator}, {RolesNames.Employee}")]
[Route("api/operation")]
[ApiController]
public class OperationController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetOperationList([FromServices] IMediator mediator)
    {
        GetOperationListQueryResult queryResult = await mediator.Send(new GetOperationListQuery());

        return queryResult.Result.Match<IActionResult>(
            data => Json(data),
            error => BadRequest());
    }

    [HttpPost]
    public async Task<IActionResult> AddOperation([FromServices] IMediator mediator, OperationDto operationDto)
    {
        AddOperationCommandResult commandResult = await mediator.Send(new AddOperationCommand(operationDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }
}
