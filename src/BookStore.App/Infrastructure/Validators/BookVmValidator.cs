using BookStore.App.Infrastructure.Mapping.Models;
using FluentValidation;

namespace BookStore.App.Infrastructure.Validators;

public class BookVmValidator : AbstractValidator<BookVm>
{
    public BookVmValidator()
    {
        RuleFor(_ => _.Name).NotEmpty();
        RuleFor(_ => _.ReleaseDate).NotEmpty();
        RuleFor(_ => _.Price).NotEmpty();
        RuleFor(_ => _.Author).NotEmpty();
    }
}