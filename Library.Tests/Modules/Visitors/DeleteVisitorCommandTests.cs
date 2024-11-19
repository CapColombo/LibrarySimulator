using Library.BLL.Modules.Visitors.Commands.DeleteVisitor;
using Library.DAL;
using Library.DAL.Models.Visitors;
using Moq;
using Moq.EntityFrameworkCore;
using OneOf.Types;

namespace Library.Tests.Modules.Visitors;

public class DeleteVisitorCommandTests
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
        DeleteVisitorCommand? command = null;

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
        DeleteVisitorCommand? command = new(id);

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
    public async Task Handle_WhenVisitorNotFound_ReturnsErrorAsync()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        DeleteVisitorCommand? command = new(id.ToString());
        _contextMock.Setup(c => c.Visitors).ReturnsDbSet([]);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<Error>());
        });

        _contextMock.Verify(c => c.Visitors, Times.Once());
    }

    [Test]
    public async Task Handle_WhenDeleteSuccess_ReturnsSuccess()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Visitor visitor = new() { Id = id, Name = "Name", Email = "Email" };
        DeleteVisitorCommand? command = new(id.ToString());
        _contextMock.Setup(c => c.Visitors).ReturnsDbSet([visitor]);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<Success>());
        });

        _contextMock.Verify(c => c.Remove(It.IsAny<Visitor>()), Times.Once());
    }
}
