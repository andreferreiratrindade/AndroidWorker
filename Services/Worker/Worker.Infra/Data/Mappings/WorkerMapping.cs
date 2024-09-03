using MongoDB.Bson.Serialization;
using Worker.Domain.Models.Entities;

namespace ActivityValidationResult.Infra.Data.Mappings
{
    public static class WorkerMapping
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<WorkerEntity>(classMap =>
            {
                classMap.MapMember(p => p.WorkerId);
            });
        }
    }
}
