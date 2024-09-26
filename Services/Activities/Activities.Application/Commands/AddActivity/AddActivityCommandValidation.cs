using FluentValidation;
using System.Text.RegularExpressions;
using Framework.Shared.IntegrationEvent.Enums;
using Framework.Core.Notifications;
using System.Security.Cryptography.X509Certificates;

namespace Activities.Application.Commands.AddActivity
{
    public class AddActivityCommandValidation : AbstractValidator<AddActivityCommand>
    {
        public AddActivityCommandValidation()
        {
            RuleFor(c => c.TimeActivityStart)
                .NotEmpty();

            RuleFor(c => c.TimeActivityEnd)
                .NotEmpty()
                .Must((a, x) => x > a.TimeActivityStart).WithMessage("TimeActivityEnd must be more than TimeActivityStart");

            var enums = Enum.GetValues(typeof(TypeActivityBuild))
                 .Cast<TypeActivityBuild>()
                 .Select(v => v)
                 .ToList();

            RuleFor(c => c.TypeActivityBuild)
                .NotEmpty()
                .Must((a, x) => enums.Any(b => b == x))
                .WithMessage($"TypeActivityBuild must be 1 - Build Component or 2 - Build Machine")
               ;

            RuleFor(c => c.Workers)
                 .NotEmpty()
                 .Must((a, x) => x.Any()).WithMessage("Workers required")
                 .Must((a, x)=>  x.Any(y=> Regex.Match(y,$"^[A-Za-z]$").Success))
                 .WithMessage("Workers must be just one letter like 'A', 'B', etc...");
        }
    }
}
