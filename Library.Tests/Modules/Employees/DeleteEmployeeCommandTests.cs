using Library.BLL.Modules.Admin.Commands.DeleteEmployee;
using Library.DAL;
using Library.DAL.Models.Employees;
using Moq;
using Moq.EntityFrameworkCore;
using OneOf.Types;

namespace Library.Tests.Modules.Employees;

public class DeleteEmployeeCommandTests
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
        DeleteEmployeeCommand? command = null;

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
        DeleteEmployeeCommand? command = new(id);

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
    public async Task Handle_WhenEmployeeNotFound_ReturnsErrorAsync()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        DeleteEmployeeCommand? command = new(id.ToString());
        _contextMock.Setup(c => c.Employees).ReturnsDbSet([]);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<Error>());
        });

        _contextMock.Verify(c => c.Employees, Times.Once());
    }

    [Test]
    public async Task Handle_WhenDeleteSuccess_ReturnsSuccess()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        Employee Employee = new() { Id = id, Name = "Name" };
        DeleteEmployeeCommand? command = new(id.ToString());
        _contextMock.Setup(c => c.Employees).ReturnsDbSet([Employee]);

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<Success>());
        });

        _contextMock.Verify(c => c.RemoveWithSaveAsync(It.IsAny<Employee>(), CancellationToken.None), Times.Once());
    }
}
