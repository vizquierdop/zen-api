using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.OfferedServices.Commands.Update
{
    public class UpdateOfferedServiceCommandValidator : AbstractValidator<UpdateOfferedServiceCommand>
    {
        public UpdateOfferedServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage("Id must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name cannot be empty.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.")
                .When(x => x.Name is not null);

            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description cannot exceed 500 characters.")
                .When(x => x.Description is not null);

            RuleFor(x => x.Duration)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Duration must be greater than or equal to 0.")
                .When(x => x.Duration.HasValue);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0.")
                .When(x => x.Price.HasValue);

        }
    }
}
