using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.OfferedServices.Commands.Create
{
    public class CreateOfferedServiceCommandValidator : AbstractValidator<CreateOfferedServiceCommand>
    {
        public CreateOfferedServiceCommandValidator()
        {

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Duration)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Duration must be greater than or equal to 0.")
                .When(x => x.Duration.HasValue);

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            // TODO Check Business belongs to User
            RuleFor(x => x.BusinessId)
                .GreaterThan(0)
                .WithMessage("BusinessId must be greater than 0.");
        }
    }
}
