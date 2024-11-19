using AutoMapper;
using Library.BLL.Modules.AutoMapper;
using Library.BLL.Modules.Dto.ResultDto;
using Library.BLL.Modules.Visitors.Queries.GetVisitorList;
using Library.DAL;
using Library.DAL.Models.Visitors;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using OneOf.Types;

namespace Library.Tests.Modules.Visitors;

public class GetVisitorListQueryTests
{
    private Mock<ILibraryContext> _contextMock;
    private Mock<IMapper> _mapperMock;
    private QueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILibraryContext>();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new VisitorProfile()));
        _mapperMock = new Mock<IMapper>();
        _mapperMock.Setup(m => m.ConfigurationProvider).Returns(configuration);
        _handler = new(_contextMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task Handle_RequestIsNull_ReturnsNotFoundAsync()
    {
        // Arrange
        GetVisitorListQuery? query = null;

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
    public async Task Handle_WhenVisitorListNotFound_ReturnsNotFoundAsync()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        GetVisitorListQuery? query = new();
        List<Visitor> visitors = [];
        _contextMock.Setup(c => c.Visitors).Returns(visitors.AsQueryable().BuildMockDbSet().Object);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<NotFound>());
        });

        _contextMock.Verify(c => c.Visitors, Times.Once());
    }

    [Test]
    public async Task Handle_WhenSaveSuccess_ReturnsData()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        GetVisitorListQuery? query = new();
        Visitor visitor = new() { Id = id, Name = "Name", Email = "Email" };
        List<Visitor> visitors = [visitor];
        _contextMock.Setup(c => c.Visitors).Returns(visitors.AsQueryable().BuildMockDbSet().Object);
        _contextMock.Setup(c => c.RentedBooks).ReturnsDbSet([]);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<IReadOnlyList<VisitorResultDto>>());
            Assert.That(actual.Result.AsT0, Has.Count.EqualTo(1));
        });

        _contextMock.Verify(c => c.Visitors, Times.Once());
    }
}
