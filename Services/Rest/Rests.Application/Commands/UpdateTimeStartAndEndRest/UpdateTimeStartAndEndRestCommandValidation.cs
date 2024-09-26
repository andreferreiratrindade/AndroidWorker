using FluentValidation;
using System.Text.RegularExpressions;
using Framework.Shared.IntegrationEvent.Enums;
using Framework.Core.Notifications;
using System.Security.Cryptography.X509Certificates;

namespace Rests.Application.Commands.UpdateTimeStartAndEndRest


{
    public class UpdateTimeStartAndEndRestCommandValidation : AbstractValidator<UpdateTimeStartAndEndRestCommand>
    {
        public UpdateTimeStartAndEndRestCommandValidation()
        {

        }
    }
}
