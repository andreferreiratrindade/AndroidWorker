using ActivityValidationResult.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ActivityValidationResult.Domain.Enums;
using MongoDB.Bson.Serialization;
using System;

namespace ActivityValidationResult.Infra.Data.Mappings
{
    public static class ActivityValidationResultMapping 
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<ActivityValidationResultEntity>(classMap =>
            {
                classMap.MapMember(p => p.Id);
                classMap.MapMember(p => p.ActivityId);
                classMap.MapMember(p => p.Status);
                classMap.MapMember(p => p.Workers);
                classMap.MapMember(p => p.Rests);
            });
        }
    }
}