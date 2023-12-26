using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivityValidationResult.Domain.Enums;
using Framework.Core.DomainObjects;
using Framework.Core.Messages;


namespace ActivityValidationResult.Domain.Models.Entities
{
    public class RestEntity
    {
        public Guid RestId { get; set; }
        public string WorkerId { get; set; }
        public TypeStatus TypeStatus { get; set; }
        public List<string> DescriptionErrors { get; }

        public RestEntity( Guid restId, string workerId, TypeStatus typeStatus, List<string> descriptionErrors)
        {
            this.DescriptionErrors = descriptionErrors;
            this.WorkerId = workerId;
            this.RestId = restId;
            this.TypeStatus = typeStatus;
        }

        public RestEntity( Guid restId, string workerId, TypeStatus typeStatus)
        {
            this.WorkerId = workerId;
            this.RestId = restId;
            this.TypeStatus = typeStatus;
        }
    }
}