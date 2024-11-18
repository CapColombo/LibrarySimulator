using AutoMapper;
using Library.BLL.Modules.Admin.AutoMapper;
using Library.BLL.Modules.Admin.Queries.GetEmployeeList;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Employees;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using OneOf.Types;

namespace Library.Tests.Modules.Employees;

public class GetEmployeeListQueryTests
{
    private Mock<ILibraryContext> _contextMock;
    private Mock<IMapper> _mapperMock;
    private QueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILibraryContext>();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new EmployeeProfile()));
        _mapperMock = new Mock<IMapper>();
        _mapperMock.Setup(m => m.ConfigurationProvider).Returns(configuration);
        _handler = new(_contextMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task Handle_RequestIsNull_ReturnsNotFoundAsync()
    {
        // Arrange
        GetEmployeeListQuery? query = null;

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<NotFound>());
        });
    }

    [Test]
    public async Task Handle_WhenEmployeeListNotFound_ReturnsNotFoundAsync()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        GetEmployeeListQuery? query = new();
        List<Employee> Employees = [];
        _contextMock.Setup(c => c.Employees).Returns(Employees.AsQueryable().BuildMockDbSet().Object);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<NotFound>());
        });

        _contextMock.Verify(c => c.Employees, Times.Once());
    }

    [Test]
    public async Task Handle_WhenSaveSuccess_ReturnsData()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        GetEmployeeListQuery? query = new();
        Employee Employee = new() { Id = id, Name = "Name", Role = new("Role") };
        List<Employee> Employees = [Employee];
        _contextMock.Setup(c => c.Employees).Returns(Employees.AsQueryable().BuildMockDbSet().Object);
        _contextMock.Setup(c => c.RentedBooks).ReturnsDbSet([]);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<IReadOnlyList<EmployeeResultDto>>());
            Assert.That(actual.Result.AsT0, Has.Count.EqualTo(1));
        });

        _contextMock.Verify(c => c.Employees, Times.Once());
    }
}
