using FluentValidation.Results;
using Framework.Core.DomainObjects;
using Framework.Core.Notifications;
using Rests.Domain.Enums;
using Rests.Domain.Rules;
using Rests.Domain.ValidationServices;

namespace Rests.Domain.Models.Entities
{
    public class Rest : Entity, IAggregateRoot
    {
        public const int TIME_IN_HOUR_TAKE_REST_TYPE_COMPONENT = 2;
        public const int TIME_IN_HOUR_TAKE_REST_TYPE_MACHINE = 4;
        public Rest(Guid activityId, string workerId, DateTime timeRestStart, TypeActivityBuild typeActivityBuild)
        {
            this.WorkerId = workerId;
            this.ActivityId = activityId;
            this.TimeRestStart = timeRestStart;
            this.TimeRestEnd = CalculateTimeRest(typeActivityBuild, timeRestStart);
            this.TypeActivityBuild = typeActivityBuild;
            this.IsAlive = true;

        }
        public Guid ActivityId { get; private set; }
        public string WorkerId { get; private set; }
        public DateTime TimeRestStart { get; private set; }
        public DateTime TimeRestEnd { get; private set; }
        public TypeActivityBuild TypeActivityBuild { get;private set;}
        public bool IsAlive {get;private set;}
        protected Rest()
        {

        }

        public static Rest Create(Guid activityId,
                                  string workerId,
                                  TypeActivityBuild typeActivityBuild,
                                  DateTime timeRestStart)
        {
            

            var rest = new Rest(activityId, workerId, timeRestStart, typeActivityBuild);

            return rest;
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

        public void UpdateTimeRestEnd( DateTime timeRestStart)
        {
            TimeRestStart = TimeRestStart;
            TimeRestEnd = CalculateTimeRest(TypeActivityBuild, TimeRestStart);

            // add event updated
        }
    }
}