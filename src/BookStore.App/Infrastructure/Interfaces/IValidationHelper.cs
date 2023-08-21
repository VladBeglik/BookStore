namespace BookStore.App.Infrastructure.Interfaces;

public interface IValidationHelper<in T>
{
    Task ValidateAndThrowAsync(T instance);
}