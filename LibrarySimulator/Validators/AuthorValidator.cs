using FluentValidation;
using Library.DAL.Models.Books;

namespace LibrarySimulator.Validators;

public class AuthorValidator : AbstractValidator<Author>
{
    public AuthorValidator()
    {
        RuleFor(v => v.Name).NotEmpty().MinimumLength(2);
    }
}