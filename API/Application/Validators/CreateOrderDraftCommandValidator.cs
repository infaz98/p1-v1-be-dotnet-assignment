using API.Application.Commands;
using FluentValidation;

namespace API.Application.Validators
{
    public class CreateOrderDraftCommandValidator : AbstractValidator<CreateOrderDraftCommand>
    {
        public CreateOrderDraftCommandValidator()
        {
            RuleFor(c => c.Quantity).GreaterThan(0);
        }
    }
}
