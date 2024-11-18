using FluentValidation.TestHelper;
using Library.DAL.Dto.Controllers;
using LibrarySimulator.Validators;

namespace Library.Tests.Validators;

public class BookValidatorTests
{
    private BookValidator? _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new BookValidator();
    }

    [Test]
    public void Validator_WhenNameNull_ShouldHaveError()
    {
        var model = new BookDto { Title = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(b => b.Title);
    }

    [Test]
    public void Validator_WhenNameLessTwoSymbols_ShouldHaveError()
    {
        var model = new BookDto { Title = "a" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(b => b.Title);
    }

    [Test]
    public void Validator_WhenNameIsSpecified_ShouldNotHaveError()
    {
        var model = new BookDto { Title = "Great Book" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(b => b.Title);
    }
}
