using FluentValidation;
using System.Text.RegularExpressions;

namespace Worker.Application.Commands.AddWorker
{
    public class AddWorkerCommandValidation : AbstractValidator<AddWorkerCommand>
    {
        public AddWorkerCommandValidation()
        {
            RuleFor(c => c.Workers)
                 .NotEmpty()
                 .Must((a, x) => x.Any()).WithMessage("Workers required")
                 .Must((a, x)=>  x.Any(y=> Regex.Match(y,$"^[A-Za-z]$").Success))
                 .WithMessage("Workers must be just one letter like 'A', 'B', etc...");
        }
    }
}
