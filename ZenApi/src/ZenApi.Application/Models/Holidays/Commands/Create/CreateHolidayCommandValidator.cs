using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.Holidays.Commands.Create
{
    public class CreateHolidayCommandValidator : AbstractValidator<CreateHolidayCommand>
    {
        public CreateHolidayCommandValidator()
        {
            RuleFor(x => x.BusinessId)
                .GreaterThan(0)
                .WithMessage("BusinessId must be greater than 0.");

            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("StartDate is required.");

            RuleFor(x => x.EndDate)
                .NotEmpty().WithMessage("EndDate is required.");

            RuleFor(x => x)
                .Must(x => x.StartDate.Date <= x.EndDate.Date)
                .WithMessage("StartDate cannot be after EndDate.");
        }
    }
}
