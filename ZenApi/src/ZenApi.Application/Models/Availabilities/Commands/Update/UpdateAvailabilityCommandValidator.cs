using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.Availabilities.Commands.Update
{
    public class UpdateAvailabilityCommandValidator : AbstractValidator<UpdateAvailabilityCommand>
    {
            private static readonly Regex TimeRegex = new(@"^([01]\d|2[0-3]):([0-5]\d)$");

            public UpdateAvailabilityCommandValidator()
            {
                RuleFor(x => x.Id)
                    .GreaterThan(0).WithMessage("AvailabilityId must be greater than 0.");

                When(x => x.IsActive, () =>
                {
                    RuleFor(x => x.Slot1Start)
                        .NotEmpty().WithMessage("Slot1Start is required when IsActive is true.")
                        .Matches(TimeRegex).WithMessage("Slot1Start must be in HH:mm format.");

                    RuleFor(x => x.Slot1End)
                        .NotEmpty().WithMessage("Slot1End is required when IsActive is true.")
                        .Matches(TimeRegex).WithMessage("Slot1End must be in HH:mm format.");

                    RuleFor(x => x)
                        .Must(x => IsStartBeforeEnd(x.Slot1Start!, x.Slot1End!))
                        .WithMessage("Slot1End must be later than Slot1Start.");
                });

                When(x => !string.IsNullOrEmpty(x.Slot2Start), () =>
                {
                    RuleFor(x => x.Slot2Start)
                        .Matches(TimeRegex).WithMessage("Slot2Start must be in HH:mm format.");

                    RuleFor(x => x)
                        .Must(x => string.IsNullOrEmpty(x.Slot2End) || IsStartBeforeEnd(x.Slot2Start!, x.Slot2End!))
                        .WithMessage("Slot2End must be later than Slot2Start.");
                });

                When(x => !string.IsNullOrEmpty(x.Slot2End), () =>
                {
                    RuleFor(x => x.Slot2End)
                        .Matches(TimeRegex).WithMessage("Slot2End must be in HH:mm format.");
                });
            }

            private bool IsStartBeforeEnd(string start, string end)
            {
                return TimeSpan.Parse(start) < TimeSpan.Parse(end);
            }
    }
}
