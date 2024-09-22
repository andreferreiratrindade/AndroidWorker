using FluentValidation;

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
