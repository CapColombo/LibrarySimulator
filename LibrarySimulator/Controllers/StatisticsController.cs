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
    public async Task<IActionResult> GetOperationStatistics([FromServices] IMediator mediator, DateTime startDate, DateTime endDate)
    {
        GetOperationStatisticsQueryResult queryResult = await mediator.Send(new GetOperationStatisticsQuery(startDate, endDate));

        return queryResult.Result.Match<IActionResult>(
            data => Json(data),
            error => BadRequest());
    }
}
