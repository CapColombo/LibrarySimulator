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
    public async Task<IActionResult> GetBookList([FromServices] IMediator mediator)
    {
        GetBookListQueryResult queryResult = await mediator.Send(new GetBookListQuery());

        return queryResult.Result.Match<IActionResult>(
            data => Json(data),
            error => BadRequest());
    }

    [HttpGet]
    public async Task<IActionResult> GetBook([FromServices] IMediator mediator, string id)
    {
        GetBookQueryResult queryResult = await mediator.Send(new GetBookQuery(id));

        return queryResult.Result.Match<IActionResult>(
            data => Json(data),
            error => BadRequest());
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromServices] IMediator mediator, BookDto bookDto)
    {
        AddBookCommandResult commandResult = await mediator.Send(new AddBookCommand(bookDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpPut]
    public async Task<IActionResult> ChangeBook([FromServices] IMediator mediator, string id, BookDto bookDto)
    {
        ChangeBookCommandResult commandResult = await mediator.Send(new ChangeBookCommand(id, bookDto));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook([FromServices] IMediator mediator, string id)
    {
        DeleteBookCommandResult commandResult = await mediator.Send(new DeleteBookCommand(id));

        return commandResult.Result.Match<IActionResult>(
            success => Ok(),
            error => BadRequest());
    }
}
