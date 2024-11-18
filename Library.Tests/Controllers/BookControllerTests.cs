using Library.BLL.Modules.Books.Commands.AddBook;
using Library.BLL.Modules.Books.Commands.ChangeBook;
using Library.BLL.Modules.Books.Commands.DeleteBook;
using Library.BLL.Modules.Books.Queries.GetBook;
using Library.BLL.Modules.Books.Queries.GetBookList;
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

public class BookControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private BookController _controller;
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
    public async Task GetAsync_ServiceReturnsJson_ReturnsOkObjectResult()
    {
        // Arrange
        var expected = new GetBookQueryResult(new BookResultDto());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetBookQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var a = await _controller.GetBookAsync(_mediatorMock.Object, _id);
        var actual = await _controller.GetBookAsync(_mediatorMock.Object, _id) as OkObjectResult;

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
        var expected = new GetBookQueryResult(new NotFound());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetBookQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetBookAsync(_mediatorMock.Object, _id) as NotFoundResult;

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
        var expected = new GetBookListQueryResult(new List<BookResultDto>());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetBookListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetBookListAsync(_mediatorMock.Object) as OkObjectResult;

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
        var expected = new GetBookListQueryResult(new NotFound());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetBookListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetBookListAsync(_mediatorMock.Object) as NotFoundResult;

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
        BookDto dto = new();
        var expected = new AddBookCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddBookAsync(_mediatorMock.Object, dto) as OkResult;

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
        BookDto dto = new();
        var expected = new AddBookCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddBookAsync(_mediatorMock.Object, dto) as BadRequestResult;

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
        BookDto dto = new();
        var expected = new ChangeBookCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.ChangeBookAsync(_mediatorMock.Object, _id, dto) as OkResult;

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
        BookDto dto = new();
        var expected = new ChangeBookCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.ChangeBookAsync(_mediatorMock.Object, _id, dto) as BadRequestResult;

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
        var expected = new DeleteBookCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.DeleteBookAsync(_mediatorMock.Object, _id) as OkResult;

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
        var expected = new DeleteBookCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteBookCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.DeleteBookAsync(_mediatorMock.Object, _id) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }
}
