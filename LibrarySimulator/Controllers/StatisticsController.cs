using Library.BLL.Modules.Statistics.Queries.GetOperationStatistics;
using Library.DAL.Models.Employees;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySimulator.Controllers;

[Authorize(Roles = $"{RolesNames.Administrator}, {RolesNames.Employee}")]
[Route("api/statistics")]
[ApiController]
public class StatisticsController : Controller
{
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetOperationStatisticsAsync([FromServices] IMediator mediator, DateTime startDate, DateTime endDate)
    {
        GetOperationStatisticsQueryResult queryResult = await mediator.Send(new GetOperationStatisticsQuery(startDate, endDate));

        return queryResult.Result.Match<IActionResult>(
            data => Ok(data),
            error => NotFound());
    }
}
