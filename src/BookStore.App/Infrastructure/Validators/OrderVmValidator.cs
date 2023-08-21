using BookStore.App.Infrastructure.Mapping.Models;
using BookStore.Domain;
using FluentValidation;

namespace BookStore.App.Infrastructure.Validators;

public class OrderVmValidator : AbstractValidator<OrderVm>
{
    public OrderVmValidator()
    {
    
    }
}