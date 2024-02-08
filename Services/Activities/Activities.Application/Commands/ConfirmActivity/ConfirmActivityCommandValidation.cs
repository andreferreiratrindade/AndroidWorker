using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class ConfirmActivityCommandValidation : AbstractValidator<ConfirmActivityCommand>
    {
        public ConfirmActivityCommandValidation()
        {
           
        }
    }
}