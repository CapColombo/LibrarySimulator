using Library.BLL.Modules.Operations.Commands.AddOperation;
using Library.BLL.Services.OperationObserver.Interfaces;
using Library.DAL;
using Library.DAL.Dto.Controllers;
using Library.DAL.Models.Enums;
using Library.DAL.Models.Statistic;
using Moq;
using OneOf.Types;

namespace Library.Tests.Modules.Operations;

public class AddOperationCommandTests
{
    private Mock<IOperationSubject> _operationDataMock;
    private Mock<IOperationObserver> _operationMock;
    private Mock<IViolationObserver> _violationMock;
    private Mock<IBookObserver> _bookMock;
    private Mock<IRentedBookObserver> _rentedBookMock;
    private Mock<IVisitorObserver> _visitorMock;
    private Mock<ILoggerObserver> _loggerMock;
    private CommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _operationDataMock = new Mock<IOperationSubject>();
        _operationMock = new Mock<IOperationObserver>();
        _violationMock = new Mock<IViolationObserver>();
        _bookMock = new Mock<IBookObserver>();
        _rentedBookMock = new Mock<IRentedBookObserver>();
        _visitorMock = new Mock<IVisitorObserver>();
        _loggerMock = new Mock<ILoggerObserver>();

        _handler = new(_operationDataMock.Object, _operationMock.Object, _violationMock.Object,
            _bookMock.Object, _rentedBookMock.Object, _visitorMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_RequestIsNull_ReturnsErrorAsync()
    {
        // Arrange
        AddOperationCommand? command = null;

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
    public async Task Handle_WhenSetOperationThrowsException_ReturnsErrorAsync()
    {
        // Arrange
        AddOperationCommand? command = new(new() { OperationType = OperationType.Returned });
        _operationDataMock.Setup(o => o.SetOperationAsync(It.IsAny<Operation>(), It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<Error>());
        });

        _operationDataMock.Verify(o => o.RegisterObserver(It.IsAny<IOperationObserver>()), Times.Exactly(6));
    }

    [Test]
    public async Task Handle_WhenSetOperationSuccess_ReturnsSuccess()
    {
        // Arrange
        AddOperationCommand? command = new(new() { OperationType = OperationType.Rented });

        // Act
        var actual = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<Success>());
        });

        _operationDataMock.Verify(o => o.RegisterObserver(It.IsAny<IOperationObserver>()), Times.Exactly(5));
    }
}
