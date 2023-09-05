using System.Data.Common;
using Activities.Domain.Models.Entities;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Activities.Domain.Models.Repositories;

namespace Activities.Infra.Data.Repository
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ActivityContext _context;

        public ActivityRepository(ActivityContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Activity activity)
        {
            _context.Activities.Add(activity);
        }

        public void Update(Activity activity)
        {
            _context.Activities.Update(activity);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

        public IQueryable<Activity> GetQueryable()
        {
            return _context.Activities.AsQueryable();
        }

        public void Delete(Guid activityId)
        {
            _context.Activities.Where(x => x.AggregateId == activityId).ExecuteDelete();
        }

        public Activity? GetById(Guid activityId) => _context.Activities.AsNoTracking().FirstOrDefault(x => x.AggregateId == activityId && x.IsAlive);
        public async Task<Activity?> GetByIdAsync(Guid activityId) => await _context.Activities.AsNoTracking().FirstOrDefaultAsync(x => x.AggregateId == activityId && x.IsAlive );
    }

}