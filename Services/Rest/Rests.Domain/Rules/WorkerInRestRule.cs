using Framework.Core.DomainObjects;
using Rests.Domain.DTOs;
using Rests.Domain.ValidationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rests.Domain.Rules
{
    public class WorkerInRestRule : IBusinessRule
    {
        private readonly IRestValidatorService _restValidatorService;
        private readonly string _workerId;
        private readonly DateTime _timStart;
        private readonly DateTime _timeEnd;
        private readonly Guid _ignoreActivityId;
        private List<string> MessageDetail = new();

        public WorkerInRestRule(IRestValidatorService restValidatorService,
                                    string workerId,
                                    DateTime timeStart,
                                    DateTime timeEnd,
                                    Guid? ignoreActivityId)
        {
            _restValidatorService = restValidatorService;
            _workerId = workerId;
            _timStart = timeStart;
            _timeEnd = timeEnd;
            _ignoreActivityId = ignoreActivityId ?? Guid.Empty;


        }

        List<string> IBusinessRule.Message => MessageDetail;

        public bool IsBroken()
        {
            var workers = _restValidatorService.GetWorkerInActivityByTimeActivityStartAndEnd(_workerId,
                                                                                                  _timStart,
                                                                                                  _timeEnd,
                                                                                                  _ignoreActivityId);
            workers ??= new List<RestDto>();
            workers.ForEach(x =>
            {
                MessageDetail.Add($"Conflits REST - Worker: '{x.WorkerId}' - Time rest start: {x.TimeRestStart} - Time rest end: {x.TimeRestEnd}; ActivityId: {x.ActivityId}");
            });
            return workers.Any();
        }

    }
}