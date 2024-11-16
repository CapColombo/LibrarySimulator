using AutoMapper;
using Library.BLL.Modules.Visitors.Queries.GetVisitor;
using Library.DAL;
using MediatR;
using Moq;

namespace LibraryTests.VisitorsTests;

public class GetVisitorTest
{
    private readonly Mock<IRequestHandler<GetVisitorQuery, GetVisitorQueryResult>> _queryHandlerMock;
    private readonly Mock<ILibraryContext> _contextMock;
    private readonly Mock<IMapper> _mapperMock;

    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
