using System.Data.Common;
using Rests.Domain.Models.Entities;
using Framework.Core.Data;
using Microsoft.EntityFrameworkCore;
using Rests.Domain.Models.Repositories;

namespace Rests.Infra.Data.Repository
{
    public class RestRepository : IRestRepository
    {
        private readonly RestContext _context;

        public RestRepository(RestContext context)
        {
            _context = context;
        }
        public IUnitOfWork UnitOfWork => _context;

        public void Add(Rest rest)
        {
            _context.Rests.Add(rest);
        }

        public void Update(Rest rest)
        {
            _context.Rests.Update(rest);

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public DbConnection GetConnection() => _context.Database.GetDbConnection();

        public  IQueryable<Rest> GetQueryable()
        {
            return  _context.Rests.AsNoTracking().AsQueryable();
        }



        public void Delete(Guid restId)
        {
            _context.Rests.Where(x => x.AggregateId ==restId).ExecuteDelete();
        }

        public Rest? GetById(Guid restId) => _context.Rests.AsNoTracking().FirstOrDefault(x => x.AggregateId == restId && x.IsAlive);
        public async Task<Rest?> GetByIdAsync(Guid restId) => await _context.Rests.AsNoTracking().FirstOrDefaultAsync(x => x.AggregateId == restId && x.IsAlive );

        public IList<Rest> GetByActivityId(Guid activityId)
        {
            return _context.Rests.Where(x => x.ActivityId == activityId ).ToList();
        }
    }

}