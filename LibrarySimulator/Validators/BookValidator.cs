using FluentValidation;
using Library.DAL.Dto.Controllers;

namespace LibrarySimulator.Validators;

public class BookValidator : AbstractValidator<BookDto>
{
    public BookValidator()
    {
        RuleFor(b => b.Title).NotEmpty().MinimumLength(5);

        RuleFor(b => b.Authors).NotEmpty();

        RuleFor(b => b.Genres).NotEmpty();

        RuleForEach(b => b.Authors).NotEmpty().SetValidator(new AuthorValidator());

        RuleForEach(b => b.Genres).NotEmpty().SetValidator(new GenreValidator());

        RuleFor(b => b.PublicationDate).NotEmpty();
    }
}
