using Library.BLL.Modules.Visitors.Commands.AddVisitor;
using Library.BLL.Modules.Visitors.Commands.ChangeVisitor;
using Library.BLL.Modules.Visitors.Commands.DeleteVisitor;
using Library.BLL.Modules.Visitors.Queries.GetVisitor;
using Library.BLL.Modules.Visitors.Queries.GetVisitorList;
using Library.DAL.Constants;
using Library.DAL.Dto.Controllers;
using Library.DAL.Dto.QueryCommandResult;
using LibrarySimulator.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OneOf.Types;
using System.Security.Claims;

namespace Library.Tests.Controllers;

public class VisitorControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private VisitorController _controller;

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
    public async Task GetAsync_ServiceReturnsJson_ReturnsOkObjectResult()
    {
        // Arrange
        string id = string.Empty;
        var expected = new GetVisitorQueryResult(new VisitorResultDto());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetVisitorQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetVisitorAsync(_mediatorMock.Object, id) as OkObjectResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task GetAsync_ServiceReturnsError_Returns404Result()
    {
        // Arrange
        string id = string.Empty;
        var expected = new GetVisitorQueryResult(new NotFound());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetVisitorQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetVisitorAsync(_mediatorMock.Object, id) as NotFoundResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        });
    }

    [Test]
    public async Task GetListAsync_ServiceReturnsJson_ReturnsOkObjectResult()
    {
        // Arrange
        var expected = new GetVisitorListQueryResult(new List<VisitorResultDto>());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetVisitorListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetVisitorListAsync(_mediatorMock.Object) as OkObjectResult;

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
        var expected = new GetVisitorListQueryResult(new NotFound());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetVisitorListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetVisitorListAsync(_mediatorMock.Object) as NotFoundResult;

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
        VisitorDto dto = new();
        var expected = new AddVisitorCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddVisitorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddVisitorAsync(_mediatorMock.Object, dto) as OkResult;

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
        VisitorDto dto = new();
        var expected = new AddVisitorCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddVisitorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddVisitorAsync(_mediatorMock.Object, dto) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }

    [Test]
    public async Task ChangeAsync_ServiceReturnsOk_ReturnsOkResult()
    {
        // Arrange
        string id = string.Empty;
        VisitorDto dto = new();
        var expected = new ChangeVisitorCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeVisitorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.ChangeVisitorAsync(_mediatorMock.Object, id, dto) as OkResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task ChangeAsync_ServiceReturnsError_Returns400Result()
    {
        // Arrange
        string id = string.Empty;
        VisitorDto dto = new();
        var expected = new ChangeVisitorCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeVisitorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.ChangeVisitorAsync(_mediatorMock.Object, id, dto) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }

    [Test]
    public async Task DeleteAsync_ServiceReturnsOk_ReturnsOkResult()
    {
        // Arrange
        string id = string.Empty;
        var expected = new DeleteVisitorCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteVisitorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.DeleteVisitorAsync(_mediatorMock.Object, id) as OkResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task DeleteAsync_ServiceReturnsError_Returns400Result()
    {
        // Arrange
        string id = string.Empty;
        var expected = new DeleteVisitorCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteVisitorCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.DeleteVisitorAsync(_mediatorMock.Object, id) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }
}
