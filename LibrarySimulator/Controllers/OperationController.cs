using Library.DAL.Dto.Controllers;
using Library.DAL.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Authorize(Roles = $"{RolesNames.Administrator}, {RolesNames.Employee}")]
[Route("api/operation")]
[ApiController]
public class OperationController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetOperationList()
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> AddOperation(OperationDto operationDto)
    {
        return null;
    }
}
