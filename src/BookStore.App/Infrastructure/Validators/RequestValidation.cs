using BookStore.App.Infrastructure.Exceptions;
using BookStore.App.Infrastructure.Interfaces;
using FluentValidation;

namespace BookStore.App.Infrastructure.Validators;

public class ValidationHelper<T> : IValidationHelper<T>
{
    private readonly IValidator<T> _validator;

    public ValidationHelper(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async Task ValidateAndThrowAsync(T instance)
    {
        var result = await _validator.ValidateAsync(instance);
        if (!result.IsValid)
        {
            var failures = result.Errors.ToList();

            if (failures.Count > 0)
            {
                throw new CustomValidationException(failures);
            }
        }
    }
}