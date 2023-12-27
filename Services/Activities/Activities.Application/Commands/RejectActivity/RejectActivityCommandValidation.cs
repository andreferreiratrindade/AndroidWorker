using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Activities.Domain.Enums;

namespace Activities.Application.Commands.RejectActivity
{
    public class RejectActivityCommandValidation : AbstractValidator<RejectActivityCommand>
    {
        public RejectActivityCommandValidation()
        {
           
        }
    }
}