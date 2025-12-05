using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.Reservations.Commands.Create
{
    public class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
    {
        private static readonly Regex TimeRegex = new(@"^([01]\d|2[0-3]):([0-5]\d)$");
        public CreateReservationCommandValidator()
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

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .When(x => x.UserId != null)
                .WithMessage("UserId must be greater than 0.");



            //  CUSTOMER / USER VALIDATION LOGIC

            // If UserId exists, Customer field must be null
            RuleFor(x => x.CustomerName)
                .Null()
                .When(x => x.UserId != null)
                .WithMessage("CustomerName must not be provided when UserId is present.");

            RuleFor(x => x.CustomerEmail)
                .Null()
                .When(x => x.UserId != null)
                .WithMessage("CustomerEmail must not be provided when UserId is present.");

            RuleFor(x => x.CustomerPhone)
                .Null()
                .When(x => x.UserId != null)
                .WithMessage("CustomerPhone must not be provided when UserId is present.");

            // If UserId does not exist, Customer fields have to be set
            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .When(x => x.UserId == null)
                .WithMessage("CustomerName is required when UserId is not provided.");

            // At least Phone or Email must be provided
            RuleFor(x => x)
                .Must(x => x.UserId != null
                           || !string.IsNullOrWhiteSpace(x.CustomerEmail)
                           || !string.IsNullOrWhiteSpace(x.CustomerPhone))
                .WithMessage("Either CustomerEmail or CustomerPhone is required when UserId is not provided.");

        }

        private bool IsStartBeforeEnd(string start, string end)
        {
            if (!TimeSpan.TryParse(start, out var s)) return false;
            if (!TimeSpan.TryParse(end, out var e)) return false;

            return s < e;
        }
    }
}
