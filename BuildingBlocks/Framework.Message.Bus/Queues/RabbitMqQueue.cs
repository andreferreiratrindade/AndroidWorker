using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Message.Bus.Queues
{
    public class RabbitMqQueue
    {
        public const string ActivitySaga = "activity-saga";
        public const string ActivityCreatedQueueName = "activity-created-queue";
        public const string ActivityRollbackQueueName = "activity-rollback-queue";
        public const string RestAddedRequestQueueName = "rest-added-queue";
        public const string RestRollBackQueueName = "rest-rollback-queue";
        public const string ActivityRequestCompletedEventQueueName = "activity-request-completed-queue";
        public const string ActivityRequestFailedEventQueueName = "activity-request-failed-queue";
    }
}
