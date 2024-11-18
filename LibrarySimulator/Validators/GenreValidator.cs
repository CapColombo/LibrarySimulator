using FluentValidation;
using Library.DAL.Models.Books;

namespace LibrarySimulator.Validators;

public class GenreValidator : AbstractValidator<Genre>
{
    public GenreValidator()
    {
        RuleFor(g => g.Title).NotEmpty().MinimumLength(5);
    }
}