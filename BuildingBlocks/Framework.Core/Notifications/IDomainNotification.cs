using FluentValidation.Results;

namespace Framework.Core.Notifications
{
    public interface IDomainNotification
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        bool HasNotifications { get; }

        bool HasNotificationWithException{get;}
        void AddNotification(Exception ex);
        void AddNotification(string key, string message);
        void AddNotification(IEnumerable<NotificationMessage> notifications);
        void AddNotification(ValidationResult validationResult);

        ValidationResult GetValidationResult();
    }
}
