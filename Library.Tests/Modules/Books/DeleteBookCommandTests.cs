using Library.BLL.Modules.Books.Commands.DeleteBook;
using Library.DAL;
using Library.DAL.Models.Books;
using Moq;
using Moq.EntityFrameworkCore;
using OneOf.Types;

namespace Library.Tests.Modules.Books;

public class DeleteBookCommandTests
{
    private Mock<ILibraryContext> _contextMock;
    private CommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILibraryContext>();
        _handler = new(_contextMock.Object);
    }

    [Test]
    public async Task Handle_RequestIsNull_ReturnsErrorAsync()
    {
        // Arrange
        DeleteBookCommand? command = null;

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<Error>());
        });
    }

    [Test]
    public async Task Handle_IdIsInvalidGuid_ReturnsErrorAsync()
    {
        // Arrange
        string id = "error";
        DeleteBookCommand? command = new(id);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<Error>());
        });
    }

    [Test]
    public async Task Handle_WhenBookNotFound_ReturnsErrorAsync()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        DeleteBookCommand? command = new(id.ToString());
        _contextMock.Setup(c => c.Books).ReturnsDbSet([]);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<Error>());
        });

        _contextMock.Verify(c => c.Books, Times.Once());
    }

    [Test]
    public async Task Handle_WhenDeleteSuccess_ReturnsSuccess()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Book Book = new() { Id = id, Title = "Title" };
        DeleteBookCommand? command = new(id.ToString());
        _contextMock.Setup(c => c.Books).ReturnsDbSet([Book]);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<Success>());
        });

        _contextMock.Verify(c => c.Remove(It.IsAny<Book>()), Times.Once());
    }
}
