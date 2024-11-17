using Library.BLL.Modules.Operations.Commands.AddOperation;
using Library.BLL.Modules.Operations.Queries.GetOperationList;
using Library.BLL.Modules.Statistics.Queries.GetOperationStatistics;
using Library.DAL.Dto.Controllers;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Employees;
using LibrarySimulator.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OneOf.Types;
using System.Security.Claims;

namespace Library.Tests.ControllersTests;

public class StatisticsControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private StatisticsController _controller;

    [SetUp]
    public void Setup()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new();

        var user = new ClaimsPrincipal(new ClaimsIdentity(
        [
                new(ClaimTypes.NameIdentifier, "testId"),
                new(ClaimTypes.Name, "testName"),
                new(ClaimTypes.Role, RolesNames.Administrator)
        ]));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
    }

    [Test]
    public async Task GetOperationStatisticsAsync_ServiceReturnsJson_ReturnsOkObjectResult()
    {
        // Arrange
        var expected = new GetOperationStatisticsQueryResult(new List<OperationStatisticsResultDto>());
        var date = DateTime.Now;
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetOperationStatisticsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetOperationStatisticsAsync(_mediatorMock.Object, date, date) as OkObjectResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task GetOperationStatisticsAsync_ServiceReturnsError_Returns404Result()
    {
        // Arrange
        var expected = new GetOperationStatisticsQueryResult(new NotFound());
        var date = DateTime.Now;
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetOperationStatisticsQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetOperationStatisticsAsync(_mediatorMock.Object, date, date) as NotFoundResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        });
    }
}
