using FluentValidation.Results;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using MassTransit;
using Rests.Domain.DomainEvents;
using Rests.Domain.Enums;
using Rests.Domain.Rules;
using Rests.Domain.ValidationServices;

namespace Rests.Domain.Models.Entities
{
    public class Rest : AggregateRoot, IAggregateRoot
    {
        public const int TIME_IN_HOUR_TAKE_REST_TYPE_COMPONENT = 48;
        public const int TIME_IN_HOUR_TAKE_REST_TYPE_MACHINE = 48;

        public Guid ActivityId { get; private set; }
        public string WorkerId { get; private set; }
        public DateTime TimeRestStart { get; private set; }
        public DateTime TimeRestEnd { get; private set; }
        public TypeActivityBuild TypeActivityBuild { get; private set; }
        public bool IsAlive { get; private set; }

        protected Rest() { }

        private Rest(Guid activityId,
                     string workerId,
                     DateTime timeRestStart,
                     TypeActivityBuild typeActivityBuild,
                     Guid? CorrelationId)
        {
            var timeRestEnd = CalculateTimeRest(typeActivityBuild, timeRestStart);
            var isAlive = true;

            var @event = new RestAddedEvent(Guid.NewGuid(),
                                            activityId,
                                            workerId,
                                            timeRestStart,
                                            timeRestEnd,
                                            typeActivityBuild,
                                            isAlive,
                                            (CorrelationId ?? Guid.Empty));
            this.RaiseEvent(@event);
        }

        public static Rest Create(Guid activityId,
                          string workerId,
                          TypeActivityBuild typeActivityBuild,
                          DateTime timeRestStart,
                          Guid? CorrelationId
                          )
        {

            var rest = new Rest(activityId, workerId, timeRestStart, typeActivityBuild, CorrelationId);
            return rest;
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case RestAddedEvent x: OnRestAddedEvent(x); break;
            }
        }

        private void OnRestAddedEvent(RestAddedEvent x)
        {
            AggregateId =x.AggregateId;
            ActivityId = x.ActivityId;
            WorkerId = x.WorkerId;
            TimeRestStart = x.TimeRestStart;
            TimeRestEnd = x.TimeRestEnd;
            TypeActivityBuild = x.TypeActivityBuild;
            IsAlive = x.IsAlive;
        }

     
        public static DateTime CalculateTimeRest(TypeActivityBuild typeActivityBuild, DateTime timeRestStart)
        {

            return typeActivityBuild switch
            {
                TypeActivityBuild.Component => CalculateTimeRestTypeActivityBuildComponent(timeRestStart),
                TypeActivityBuild.Machine => CalculateTimeRestTypeActivityBuildMachinet(timeRestStart)
            };
        }

        public static DateTime CalculateTimeRestTypeActivityBuildComponent(DateTime timeActivityEnd)
        {
            return timeActivityEnd.AddHours(TIME_IN_HOUR_TAKE_REST_TYPE_COMPONENT);
        }
        public static DateTime CalculateTimeRestTypeActivityBuildMachinet(DateTime timeActivityEnd)
        {
            return timeActivityEnd.AddHours(TIME_IN_HOUR_TAKE_REST_TYPE_MACHINE);
        }

        public void UpdateTimeRestEnd(DateTime timeRestStart)
        {
            TimeRestStart = TimeRestStart;
            TimeRestEnd = CalculateTimeRest(TypeActivityBuild, TimeRestStart);

            // add event updated
        }

    }
}