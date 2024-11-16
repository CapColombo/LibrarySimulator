using Library.DAL.Dto.Controllers;
using Library.DAL.Models.Employees;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Authorize(Roles = $"{RolesNames.Administrator}")]
[Route("api/admin")]
[ApiController]
public class AdminController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetEmployeesList()
    {
        return null;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployee(string id)
    {
        return null;
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployee(string id)
    {
        return null;
    }

    [HttpPut]
    public async Task<IActionResult> ChangeEmployee(EmployeeDto employeeDto)
    {
        return null;
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        return null;
    }
}
