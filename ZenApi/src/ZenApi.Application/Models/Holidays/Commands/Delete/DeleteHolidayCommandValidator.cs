using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenApi.Application.Common.Interfaces;

namespace ZenApi.Application.Models.Holidays.Commands.Delete
{
    public class DeleteHolidayCommandValidator : AbstractValidator<DeleteHolidayCommand>
    {

        public DeleteHolidayCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage(x => $"The holiday register with Id {x.Id} does not exist");
        }
    }
}
