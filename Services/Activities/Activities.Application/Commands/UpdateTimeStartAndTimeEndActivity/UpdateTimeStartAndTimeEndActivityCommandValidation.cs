using FluentValidation;

namespace Activities.Application.Commands.UpdateTimeStartAndTimeEndActivity
{
    public class UpdateTimeStartAndTimeEndActivityCommandValidation : AbstractValidator<UpdateTimeStartAndTimeEndActivityCommand>
    {
        public UpdateTimeStartAndTimeEndActivityCommandValidation()
        {
            RuleFor(c => c.TimeActivityStart)
                .NotEmpty();

            RuleFor(c => c.TimeActivityEnd)
                .NotEmpty()
                .Must((a, x) => x > a.TimeActivityStart).WithMessage("TimeActivityEnd must be more than TimeActivityStart");

            RuleFor(c => c.ActivityId)
                 .NotEmpty();
        }
    }
}
