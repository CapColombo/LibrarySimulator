using Library.DAL.Dto.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Route("api/book")]
[ApiController]
public class BookController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetBookList()
    {
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> GetBook(string id)
    {
        return null;
    }

    [HttpPut]
    public async Task<IActionResult> ChangeBook(BookDto bookDto)
    {
        return null;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBook(string id)
    {
        return null;
    }
}
