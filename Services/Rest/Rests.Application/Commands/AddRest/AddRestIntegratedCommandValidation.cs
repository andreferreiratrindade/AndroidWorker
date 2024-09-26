using FluentValidation;
using System.Text.RegularExpressions;
using Framework.Shared.IntegrationEvent.Enums;
using Framework.Core.Notifications;
using System.Security.Cryptography.X509Certificates;

namespace Rests.Application.Commands.AddRest
{
    public class AddRestIntegratedCommandValidation : AbstractValidator<AddRestIntegratedCommand>
    {
        public AddRestIntegratedCommandValidation()
        {

        }
    }
}
