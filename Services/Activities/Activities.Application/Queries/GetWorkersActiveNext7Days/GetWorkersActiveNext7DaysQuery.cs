using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.DomainObjects;
using Activities.Domain.Models.Repositories;
using Activities.Domain.DTO;
using Dapper;
using Activities.Domain.Models.Entities;
using Framework.Core.Messages;

namespace Activities.Application.Queries
{
    public class GetWorkersActiveNext7DaysQuery  : IQuery<List<WorkActiveReportDto>>
    {
        public  DateTime DateReference;

        public GetWorkersActiveNext7DaysQuery(DateTime dateReference)
        {
           DateReference = dateReference;
        }
    }}