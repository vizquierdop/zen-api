using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.Reservations.Commands.Delete
{
    public class DeleteReservationCommandValidator : AbstractValidator<DeleteReservationCommand>
    {
        public DeleteReservationCommandValidator() 
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage(x => $"The reservation with Id {x.Id} does not exist");
        }
    }
}
