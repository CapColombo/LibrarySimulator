﻿using AutoMapper;
using Library.BLL.Modules.Books.AutoMapper;
using Library.BLL.Modules.Books.Queries.GetBookList;
using Library.DAL;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Books;
using MockQueryable.Moq;
using Moq;
using Moq.EntityFrameworkCore;
using OneOf.Types;

namespace Library.Tests.Modules.Books;

public class GetBookListQueryTests
{
    private Mock<ILibraryContext> _contextMock;
    private Mock<IMapper> _mapperMock;
    private QueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _contextMock = new Mock<ILibraryContext>();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new BookProfile()));
        _mapperMock = new Mock<IMapper>();
        _mapperMock.Setup(m => m.ConfigurationProvider).Returns(configuration);
        _handler = new(_contextMock.Object, _mapperMock.Object);
    }

    [Test]
    public async Task Handle_RequestIsNull_ReturnsNotFoundAsync()
    {
        // Arrange
        GetBookListQuery? query = null;

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
    public async Task Handle_WhenBookListNotFound_ReturnsNotFoundAsync()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        GetBookListQuery? query = new();
        List<Book> books = [];
        _contextMock.Setup(c => c.Books).Returns(books.AsQueryable().BuildMockDbSet().Object);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT1, Is.True);
            Assert.That(actual.Result.AsT1, Is.InstanceOf<NotFound>());
        });

        _contextMock.Verify(c => c.Books, Times.Once());
    }

    [Test]
    public async Task Handle_WhenSaveSuccess_ReturnsData()
    {
        // Arrange
        Guid id = Guid.NewGuid();
        GetBookListQuery? query = new();
        Book book = new() { Id = id, Title = "Title" };
        List<Book> books = [book];
        _contextMock.Setup(c => c.Books).Returns(books.AsQueryable().BuildMockDbSet().Object);
        _contextMock.Setup(c => c.RentedBooks).ReturnsDbSet([]);

        // Act
        var actual = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Result.IsT0, Is.True);
            Assert.That(actual.Result.AsT0, Is.InstanceOf<IReadOnlyList<BookResultDto>>());
            Assert.That(actual.Result.AsT0, Has.Count.EqualTo(1));
        });

        _contextMock.Verify(c => c.Books, Times.Once());
    }
}
