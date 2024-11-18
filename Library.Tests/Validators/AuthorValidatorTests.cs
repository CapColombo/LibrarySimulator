using FluentValidation.TestHelper;
using Library.DAL.Models.Books;
using LibrarySimulator.Validators;

namespace Library.Tests.Validators;

public class AuthorValidatorTests
{
    private AuthorValidator? _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new AuthorValidator();
    }

    [Test]
    public void Validator_WhenNameNull_ShouldHaveError()
    {
        var model = new Author { Name = null };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.Name);
    }

    [Test]
    public void Validator_WhenNameLessTwoSymbols_ShouldHaveError()
    {
        var model = new Author { Name = "a" };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.Name);
    }

    [Test]
    public void Validator_WhenNameIsSpecified_ShouldNotHaveError()
    {
        var model = new Author { Name = "Jeremy" };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(person => person.Name);
    }
}
