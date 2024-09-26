using FluentValidation;
using System.Text.RegularExpressions;
using Framework.Shared.IntegrationEvent.Enums;
using Framework.Core.Notifications;
using System.Security.Cryptography.X509Certificates;

namespace ActivityValidationResult.Application.Commands.UpdateActivityConfirmed

{
    public class UpdateActivityConfirmedCommandValidation : AbstractValidator<UpdateActivityConfirmedCommand>
    {
        public UpdateActivityConfirmedCommandValidation()
        {

        }
    }
}
