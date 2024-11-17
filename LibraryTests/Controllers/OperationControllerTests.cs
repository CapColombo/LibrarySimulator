using Library.BLL.Modules.Operations.Commands.AddOperation;
using Library.BLL.Modules.Operations.Queries.GetOperationList;
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

public class OperationControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private OperationController _controller;
    private string _id;

    [SetUp]
    public void Setup()
    {
        _id = string.Empty;
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
    public async Task GetListAsync_ServiceReturnsJson_ReturnsOkObjectResult()
    {
        // Arrange
        var expected = new GetOperationListQueryResult(new List<OperationResultDto>());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetOperationListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetOperationListAsync(_mediatorMock.Object) as OkObjectResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task GetListAsync_ServiceReturnsError_Returns404Result()
    {
        // Arrange
        var expected = new GetOperationListQueryResult(new NotFound());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetOperationListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetOperationListAsync(_mediatorMock.Object) as NotFoundResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        });
    }

    [Test]
    public async Task AddAsync_ServiceReturnsOk_ReturnsOkResult()
    {
        // Arrange
        OperationDto dto = new();
        var expected = new AddOperationCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddOperationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddOperationAsync(_mediatorMock.Object, dto) as OkResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task AddAsync_ServiceReturnsError_Returns400Result()
    {
        // Arrange
        OperationDto dto = new();
        var expected = new AddOperationCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddOperationCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddOperationAsync(_mediatorMock.Object, dto) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }
}
