using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZenApi.Application.Dtos.Availabilities;

namespace ZenApi.Application.Models.Availabilities.Commands.BulkUpdate
{
    public class BulkUpdateAvailabilitiesCommandValidator : AbstractValidator<BulkUpdateAvailabilitiesCommand>
    {
        public BulkUpdateAvailabilitiesCommandValidator()
        {
            RuleFor(x => x.BusinessId)
                .GreaterThan(0).WithMessage("BusinessId required.");

            RuleFor(x => x.Availabilities)
                .NotEmpty().WithMessage("At least one availability record is required.");

            // Validar cada elemento de la lista
            RuleForEach(x => x.Availabilities).SetValidator(new AvailabilityDtoValidator());
        }
    }

    public class AvailabilityDtoValidator : AbstractValidator<BulkUpdateAvailabilitiesDto>
    {
        private static readonly Regex TimeRegex = new(@"^([01]\d|2[0-3]):([0-5]\d)$");

        public AvailabilityDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);

            When(x => x.IsActive, () =>
            {
                RuleFor(x => x.Slot1Start)
                    .NotEmpty().WithMessage("Slot1Start is required when active.")
                    .Matches(TimeRegex).WithMessage("Format HH:mm required.");

                RuleFor(x => x.Slot1End)
                    .NotEmpty().WithMessage("Slot1End is required when active.")
                    .Matches(TimeRegex).WithMessage("Format HH:mm required.");

                RuleFor(x => x)
                    .Must(x => IsStartBeforeEnd(x.Slot1Start!, x.Slot1End!))
                    .WithMessage("Slot1End must be later than Slot1Start.");
            });

            When(x => !string.IsNullOrEmpty(x.Slot2Start), () =>
            {
                RuleFor(x => x.Slot2Start).Matches(TimeRegex);

                RuleFor(x => x)
                    .Must(x => string.IsNullOrEmpty(x.Slot2End) || IsStartBeforeEnd(x.Slot2Start!, x.Slot2End!))
                    .WithMessage("Slot2End must be later than Slot2Start.");
            });

            When(x => !string.IsNullOrEmpty(x.Slot2End), () =>
            {
                RuleFor(x => x.Slot2End).Matches(TimeRegex);
            });
        }

        private bool IsStartBeforeEnd(string start, string end)
        {
            if (string.IsNullOrEmpty(start) || string.IsNullOrEmpty(end)) return false;
            return TimeSpan.Parse(start) < TimeSpan.Parse(end);
        }
    }
}
