using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.Reservations.Commands.Update
{
    public class UpdateReservationCommandValidator : AbstractValidator<UpdateReservationCommand>
    {
        private static readonly Regex TimeRegex = new(@"^([01]\d|2[0-3]):([0-5]\d)$");
        public UpdateReservationCommandValidator() 
        {
            // Date
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date is required.");

            // Time format HH:mm
            RuleFor(x => x.StartTime)
                .NotEmpty()
                .Matches(TimeRegex)
                .WithMessage("StartTime must be in HH:mm format.");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .Matches(TimeRegex)
                .WithMessage("EndTime must be in HH:mm format.");

            // Start < End
            RuleFor(x => x)
                .Must(x => IsStartBeforeEnd(x.StartTime, x.EndTime))
                .WithMessage("StartTime must be earlier than EndTime.");

            // Relations
            RuleFor(x => x.ServiceId)
                .GreaterThan(0)
                .WithMessage("ServiceId must be greater than 0.");
        }

        private bool IsStartBeforeEnd(string start, string end)
        {
            if (!TimeSpan.TryParse(start, out var s)) return false;
            if (!TimeSpan.TryParse(end, out var e)) return false;

            return s < e;
        }
    }
}
