using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.DeleteActivity
{
    public class DeleteActivityCommandValidation : AbstractValidator<DeleteActivityCommand>
    {
        public DeleteActivityCommandValidation()
        {
            RuleFor(c => c.ActivityId)
                .NotEmpty();

        }
    }
}