using Library.BLL.Modules.Operations.Commands.AddOperation;
using Library.BLL.Modules.Operations.Queries.GetOperationList;
using Library.DAL.Constants;
using Library.DAL.Dto.Controllers;
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
    [Route("list")]
    public async Task<IActionResult> GetOperationListAsync([FromServices] IMediator mediator)
    {
        GetOperationListQueryResult queryResult = await mediator.Send(new GetOperationListQuery());

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            error => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> AddOperationAsync([FromServices] IMediator mediator, OperationDto operationDto)
    {
        AddOperationCommandResult commandResult = await mediator.Send(new AddOperationCommand(operationDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }
}
