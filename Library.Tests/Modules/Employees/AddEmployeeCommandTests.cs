using Library.BLL.Modules.Admin.Commands.AddEmployee;
using Library.DAL;
using LibrarySimulator.Controllers.Dto;
using Moq;
using OneOf.Types;

namespace Library.Tests.Modules.Employees;

public class AddEmployeeCommandTests
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
        AddEmployeeCommand? command = null;

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
    public async Task Handle_WhenSaveSuccess_ReturnsSuccess()
    {
        // Arrange
        AddEmployeeCommand? command = new(new EmployeeDto() { Role = new("name") });

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<Success>());
        });
    }
}
