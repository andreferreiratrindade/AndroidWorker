

namespace Activities.Domain.DTO
{
    public class WorkActiveReportDto
    {
        public WorkActiveReportDto()
        {
            Activities = new List<WorkActiviteActivityReportDto>();
        }
        public string WorkerId {get;set;}
       public List<WorkActiviteActivityReportDto> Activities {get;set;}
    }
}
