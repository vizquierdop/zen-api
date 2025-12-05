using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenApi.Application.Models.BusinessCategories.Commands.Delete
{
    public class DeleteBusinessCategoryCommandValidator
        : AbstractValidator<DeleteBusinessCategoryCommand>
    {
        public DeleteBusinessCategoryCommandValidator()
        {
            RuleFor(x => x.BusinessId)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }
    }
}
