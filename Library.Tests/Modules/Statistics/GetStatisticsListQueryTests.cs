using AutoMapper;
using Library.BLL.Modules.AutoMapper;
using Library.BLL.Modules.Dto.ResultDto;
using Library.BLL.Modules.Statistics.Queries.GetOperationStatistics;
using Library.DAL;
using Library.DAL.Models.Statistic;
using MockQueryable.Moq;
using Moq;
using OneOf.Types;

namespace Library.Tests.Modules.Statistics;

public class GetStatisticsListQueryTests
{
    private Mock<ILibraryContext> _contextMock;
    private Mock<IMapper> _mapperMock;
    private QueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILibraryContext>();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new OperationStatisticsProfile()));
        _mapperMock = new Mock<IMapper>();
        _mapperMock.Setup(m => m.ConfigurationProvider).Returns(configuration);
        _handler = new(_contextMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task Handle_RequestIsNull_ReturnsNotFoundAsync()
    {
        // Arrange
        GetOperationStatisticsQuery? query = null;

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
    public async Task Handle_WhenOperationListNotFound_ReturnsNotFoundAsync()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        DateTime date = DateTime.UtcNow;
        GetOperationStatisticsQuery? query = new(date, date);
        List<Operation> operations = [];
        _contextMock.Setup(c => c.Operations).Returns(operations.AsQueryable().BuildMockDbSet().Object);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<NotFound>());
        });

        _contextMock.Verify(c => c.Operations, Times.Once());
    }

    [Test]
    public async Task Handle_WhenSaveSuccess_ReturnsData()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        DateTime startDate = DateTime.Today.AddDays(-1);
        DateTime endDate = DateTime.Today.AddDays(1);
        GetOperationStatisticsQuery? query = new(startDate, endDate);
        Operation operation = new() { Id = id, Visitor = new("name", "email"), Book = new() { Title = "title" }, Date = DateTime.Today };
        List<Operation> operations = [operation];
        _contextMock.Setup(c => c.Operations).Returns(operations.AsQueryable().BuildMockDbSet().Object);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<IReadOnlyList<OperationStatisticsResultDto>>());
            Assert.That(actual.Result.AsT0, Has.Count.EqualTo(1));
        });

        _contextMock.Verify(c => c.Operations, Times.Once());
    }
}
