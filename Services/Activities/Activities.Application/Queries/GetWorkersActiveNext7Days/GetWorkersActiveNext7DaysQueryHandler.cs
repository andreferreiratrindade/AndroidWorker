using Activities.Domain.Models.Repositories;
using Activities.Domain.DTO;
using Dapper;
using Framework.Core.Messages;
using Activities.Domain.Enums;

namespace Activities.Application.Queries
{
    public class GetWorkersActiveNext7DaysQueryHandler : IQueryHandler<GetWorkersActiveNext7DaysQuery, List<WorkActiveReportDto>>
    {

        private readonly IActivityRepository _activityRepository;
        private const int WORKERS_BY_DAYS = 7;

        public GetWorkersActiveNext7DaysQueryHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task<List<WorkActiveReportDto>> Handle(GetWorkersActiveNext7DaysQuery request, CancellationToken cancellationToken)
        {
            var tt = _activityRepository.GetQueryable().Select(x=> x.AggregateId).ToList();

            DateTime dateNext7Days = request.DateReference.AddDays(WORKERS_BY_DAYS);

            const string sql = @"SELECT TOP 10
                                        activities.Id as ActivityId,
                                        workersActivity.WorkerId,
                                        activities.TimeActivityStart,
                                        activities.TimeActivityEnd,
                                        activities.TimeRestEnd,
                                        activities.TypeActivityBuild
                                FROM  Activities activities
                                inner join WorkersActivity workersActivity on activities.Id = workersActivity.ActivityId

                                where  activities.IsAlive = 1
                                and
                                not(
                                        
                                        ( @DateReference < activities.TimeActivityStart and @dateNext7Days < activities.TimeActivityStart)
                                        or
                                        ( @DateReference > activities.TimeRestEnd and @DateReference > activities.TimeRestEnd)
                                ) 
                                order by  activities.TimeActivityStart
                                ";
            var lst = await _activityRepository.GetConnection().QueryAsync<dynamic>(
                    sql,
                    new { request.DateReference, dateNext7Days });

            return MapReport(lst); ;
        }

        private static List<WorkActiveReportDto> MapReport(IEnumerable<dynamic> lst)
        {
            var lstDic = lst.GroupBy(x => x.WorkerId, x => x).ToDictionary(x => x.Key, x => x);
            var lstDTO = lstDic.Select(x =>
                    new WorkActiveReportDto
                    {
                        WorkerId = x.Key,
                        Activities = x.Value.Select(y =>
                                        new WorkActiviteActivityReportDto
                                        {
                                            ActivityId = y.ActivityId.GetType() == Guid.Empty.GetType() ? y.ActivityId : new Guid((string)y.ActivityId),
                                            TimeActivityStart = y.TimeActivityStart.GetType() == new DateTime().GetType() ? y.TimeActivityStart : DateTime.Parse(y.TimeActivityStart),
                                            TimeActivityEnd = y.TimeActivityEnd.GetType() == new DateTime().GetType() ? y.TimeActivityEnd : DateTime.Parse(y.TimeActivityEnd),
                                            TimeRestEnd = y.TimeRestEnd.GetType() == new DateTime().GetType() ? y.TimeRestEnd : DateTime.Parse(y.TimeRestEnd),
                                            TypeActivityBuild = (TypeActivityBuild)Enum.ToObject(typeof(TypeActivityBuild), y.TypeActivityBuild)
                                        }).ToList()
                    }).ToList();

            return lstDTO;
        }
    }
}