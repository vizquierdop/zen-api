using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.OfferedServices.Commands.Delete
{
    public class DeleteOfferedServiceCommandValidator : AbstractValidator<DeleteOfferedServiceCommand>
    {
        public DeleteOfferedServiceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage(x => $"The service with Id {x.Id} does not exist");
        }
    }
}
