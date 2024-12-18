﻿using Library.BLL.Modules.Admin.Commands.AddEmployee;
using Library.BLL.Modules.Admin.Commands.ChangeEmployee;
using Library.BLL.Modules.Admin.Commands.DeleteEmployee;
using Library.BLL.Modules.Admin.Queries.GetEmployee;
using Library.BLL.Modules.Admin.Queries.GetEmployeeList;
using Library.BLL.Modules.Dto.ResultDto;
using Library.DAL.Constants;
using LibrarySimulator.Controllers;
using LibrarySimulator.Controllers.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OneOf.Types;
using System.Security.Claims;

namespace Library.Tests.Controllers;

public class AdminControllerTests
{
    private Mock<IMediator> _mediatorMock;
    private AdminController _controller;
    private string _id;

    [SetUp]
    public void Setup()
    {
        _id = string.Empty;
        _mediatorMock = new Mock<IMediator>();
        _controller = new();

        var user = new ClaimsPrincipal(new ClaimsIdentity(
            [
                 new(ClaimTypes.NameIdentifier, "testId"),
                 new(ClaimTypes.Name, "testName"),
                 new(ClaimTypes.Role, RolesNames.Administrator)
            ]));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [TearDown]
    public void TearDown()
    {
        _controller.Dispose();
    }

    [Test]
    public async Task GetAsync_ServiceReturnsJson_ReturnsOkObjectResult()
    {
        // Arrange
        var expected = new GetEmployeeQueryResult(new EmployeeResultDto());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetEmployeeQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var a = await _controller.GetEmployeeAsync(_mediatorMock.Object, _id);
        var actual = await _controller.GetEmployeeAsync(_mediatorMock.Object, _id) as OkObjectResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task GetAsync_ServiceReturnsError_Returns404Result()
    {
        // Arrange
        var expected = new GetEmployeeQueryResult(new NotFound());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetEmployeeQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetEmployeeAsync(_mediatorMock.Object, _id) as NotFoundResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        });
    }

    [Test]
    public async Task GetListAsync_ServiceReturnsJson_ReturnsOkObjectResult()
    {
        // Arrange
        var expected = new GetEmployeeListQueryResult(new List<EmployeeResultDto>());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetEmployeeListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetEmployeeListAsync(_mediatorMock.Object) as OkObjectResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task GetListAsync_ServiceReturnsError_Returns404Result()
    {
        // Arrange
        var expected = new GetEmployeeListQueryResult(new NotFound());
        _mediatorMock.Setup(x => x.Send(It.IsAny<GetEmployeeListQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.GetEmployeeListAsync(_mediatorMock.Object) as NotFoundResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        });
    }

    [Test]
    public async Task AddAsync_ServiceReturnsOk_ReturnsOkResult()
    {
        // Arrange
        EmployeeDto dto = new();
        var expected = new AddEmployeeCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddEmployeeCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddEmployeeAsync(_mediatorMock.Object, dto) as OkResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task AddAsync_ServiceReturnsError_Returns400Result()
    {
        // Arrange
        EmployeeDto dto = new();
        var expected = new AddEmployeeCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<AddEmployeeCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.AddEmployeeAsync(_mediatorMock.Object, dto) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }

    [Test]
    public async Task ChangeAsync_ServiceReturnsOk_ReturnsOkResult()
    {
        // Arrange
        EmployeeDto dto = new();
        var expected = new ChangeEmployeeCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeEmployeeCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.ChangeEmployeeAsync(_mediatorMock.Object, _id, dto) as OkResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task ChangeAsync_ServiceReturnsError_Returns400Result()
    {
        // Arrange
        EmployeeDto dto = new();
        var expected = new ChangeEmployeeCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<ChangeEmployeeCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.ChangeEmployeeAsync(_mediatorMock.Object, _id, dto) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }

    [Test]
    public async Task DeleteAsync_ServiceReturnsOk_ReturnsOkResult()
    {
        // Arrange
        var expected = new DeleteEmployeeCommandResult(new Success());
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteEmployeeCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.DeleteEmployeeAsync(_mediatorMock.Object, _id) as OkResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        });
    }

    [Test]
    public async Task DeleteAsync_ServiceReturnsError_Returns400Result()
    {
        // Arrange
        var expected = new DeleteEmployeeCommandResult(new Error());
        _mediatorMock.Setup(x => x.Send(It.IsAny<DeleteEmployeeCommand>(), It.IsAny<CancellationToken>())).ReturnsAsync(expected);

        // Act
        var actual = await _controller.DeleteEmployeeAsync(_mediatorMock.Object, _id) as BadRequestResult;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        });
    }
}
