namespace BookStore.App.Infrastructure.Exceptions;

public class NotFoundException : Exception, ICustomExceptionMarker
{
    public NotFoundException() : base("Ошибка сервера") { }

    public NotFoundException(string message) : base(message) { }
}
