using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Framework.Core.Messages;
using Framework.Core.Notifications;

namespace Framework.Core.DomainObjects
{
    public static class BusinessRuleValidation
    {

        public static List<NotificationMessage> Check(IBusinessRule rule)
        {
            var lstNotifications = new List<NotificationMessage>();
            if (rule.IsBroken())
            {
                rule.Message.ForEach(x => lstNotifications.Add(new NotificationMessage(string.Empty, x)));
            }

            return lstNotifications;
        }
    }
}