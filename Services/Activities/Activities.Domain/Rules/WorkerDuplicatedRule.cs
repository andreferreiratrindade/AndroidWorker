using FluentValidation;
using Framework.Core.DomainObjects;
using System.Data;

namespace Activities.Domain.Rules
{
    public class WorkerDuplicatedRule :IBusinessRule
    {
        private List<string> MessageDetail = new();


        private readonly List<string> _workers;

        public WorkerDuplicatedRule( List<string> workers)
        {
            _workers = workers;
        }


        List<string> IBusinessRule.Message =>MessageDetail;


        public bool IsBroken()
        {
             IEnumerable<string> duplicates = _workers.GroupBy(x => x)
                                        .Where(g => g.Count() > 1)
                                        .Select(x => x.Key);
            MessageDetail.Add($"The workes are duplicated: {string.Join(",", duplicates)}");
            
            return duplicates.Any() ;
        }
    }
}
