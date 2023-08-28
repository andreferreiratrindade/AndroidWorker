using Framework.Core.DomainObjects;
using Activities.Domain.ValidatorServices;
using Activities.Domain.DTO;

namespace Activities.Domain.Rules
{
    public class WorkerInActivityRule : IBusinessRule
    {
        private readonly IActivityValidatorService _activityValidatorService;
        private readonly List<string> _workers;
        private readonly DateTime _timeActivityStart;
        private readonly DateTime _timeActivityEnd;
        private readonly Guid _ignoreActivityId;
        private List<string> MessageDetail = new();

        public WorkerInActivityRule(IActivityValidatorService activityValidatorService,
                                    List<string> workers,
                                    DateTime timeActivityStart,
                                    DateTime timeActivityEnd,
                                    Guid? ignoreActivityId)
        {
            _activityValidatorService = activityValidatorService;
            _workers = workers;
            _timeActivityStart = timeActivityStart;
            _timeActivityEnd = timeActivityEnd;
            _ignoreActivityId = ignoreActivityId ?? Guid.Empty;


        }

        List<string> IBusinessRule.Message =>MessageDetail;

        public bool IsBroken()
        {
            var workers = _activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(_workers,
                                                                                                  _timeActivityStart,
                                                                                                  _timeActivityEnd,
                                                                                                  _ignoreActivityId);
            workers ??= new List<ActivityWithWorkerDto>();
            workers.ForEach(x =>
            {
                MessageDetail.Add($"Conflits Activity - Worker: '{x.WorkerId}' - Time start: {x.TimeActivityStart} - Time end: {x.TimeActivityEnd} - ActivityId: {x.ActivityId}");
            });
            return workers.Any();
        }

    }
}