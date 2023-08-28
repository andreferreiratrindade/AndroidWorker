using Framework.Core.DomainObjects;
using Activities.Domain.Enums;

namespace Activities.Domain.Rules
{
    public class ActivityWithTypeBuildComponentAcceptJustOneWorkerRule : IBusinessRule
    {
        private List<string> MessageDetail = new();


        private readonly TypeActivityBuild _typeActivityBuild;
        private readonly List<string> _workers;

        public ActivityWithTypeBuildComponentAcceptJustOneWorkerRule(TypeActivityBuild typeActivityBuild, List<string> workers)
        {

            _typeActivityBuild = typeActivityBuild;
            _workers = workers;

            MessageDetail.Add($"The TypeActivityBuild equals 'Component' accept just one Worker");
        }

        List<string> IBusinessRule.Message => MessageDetail;

        public bool IsBroken()
        {
            return _typeActivityBuild == Enums.TypeActivityBuild.Component
                && _workers.Count > 1;
        }
    }
}