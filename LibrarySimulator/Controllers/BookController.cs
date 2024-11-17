using Library.BLL.Modules.Books.Commands.AddBook;
using Library.BLL.Modules.Books.Commands.ChangeBook;
using Library.BLL.Modules.Books.Commands.DeleteBook;
using Library.BLL.Modules.Books.Queries.GetBook;
using Library.BLL.Modules.Books.Queries.GetBookList;
using Library.DAL.Dto.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Route("api/book")]
[ApiController]
public class BookController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetBookListAsync([FromServices] IMediator mediator)
    {
        GetBookListQueryResult queryResult = await mediator.Send(new GetBookListQuery());

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            error => NotFound());
    }

    [HttpGet]
    public async Task<IActionResult> GetBookAsync([FromServices] IMediator mediator, string id)
    {
        GetBookQueryResult queryResult = await mediator.Send(new GetBookQuery(id));

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            error => NotFound());
    }

    [HttpPost]
    public async Task<IActionResult> AddBookAsync([FromServices] IMediator mediator, BookDto bookDto)
    {
        AddBookCommandResult commandResult = await mediator.Send(new AddBookCommand(bookDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpPut]
    public async Task<IActionResult> ChangeBookAsync([FromServices] IMediator mediator, string id, BookDto bookDto)
    {
        ChangeBookCommandResult commandResult = await mediator.Send(new ChangeBookCommand(id, bookDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBookAsync([FromServices] IMediator mediator, string id)
    {
        DeleteBookCommandResult commandResult = await mediator.Send(new DeleteBookCommand(id));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }
}
