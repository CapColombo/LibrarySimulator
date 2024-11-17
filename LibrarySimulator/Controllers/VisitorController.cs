using Library.BLL.Modules.Visitors.Commands.AddVisitor;
using Library.BLL.Modules.Visitors.Commands.ChangeVisitor;
using Library.BLL.Modules.Visitors.Commands.DeleteVisitor;
using Library.BLL.Modules.Visitors.Queries.GetVisitor;
using Library.BLL.Modules.Visitors.Queries.GetVisitorList;
using Library.DAL.Dto.Controllers;
using Library.DAL.Models.Employees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Authorize(Roles = $"{RolesNames.Administrator}, {RolesNames.Employee}")]
[Route("api/visitor")]
[ApiController]
public class VisitorController : Controller
{
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetVisitorListAsync([FromServices] IMediator mediator)
    {
        GetVisitorListQueryResult queryResult = await mediator.Send(new GetVisitorListQuery());

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            notFound => NotFound());
    }

    [HttpGet]
    public async Task<IActionResult> GetVisitorAsync([FromServices] IMediator mediator, string id)
    {
        GetVisitorQueryResult queryResult = await mediator.Send(new GetVisitorQuery(id));

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            notFound => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> AddVisitorAsync([FromServices] IMediator mediator, VisitorDto visitorDto)
    {
        AddVisitorCommandResult commandResult = await mediator.Send(new AddVisitorCommand(visitorDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpPut]
    public async Task<IActionResult> ChangeVisitorAsync([FromServices] IMediator mediator, string id, VisitorDto visitorDto)
    {
        ChangeVisitorCommandResult commandResult = await mediator.Send(new ChangeVisitorCommand(id, visitorDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteVisitorAsync([FromServices] IMediator mediator, string id)
    {
        DeleteVisitorCommandResult commandResult = await mediator.Send(new DeleteVisitorCommand(id));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }
}
