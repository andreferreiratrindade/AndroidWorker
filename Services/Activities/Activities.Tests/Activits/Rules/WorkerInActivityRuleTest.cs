using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Activities.Domain.DTO;
using Activities.Domain.Enums;
using Activities.Domain.Models.Entities;
using Activities.Domain.Rules;
using Activities.Domain.ValidatorServices;

namespace Activities.Tests.Activits
{
    public class WorkerInActivityRuleTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldValideWorkerWithoutActivity()
        {

            var activityValidatorService = Substitute.For<IActivityValidatorService>();
            var request = new {Workers = new List<string>(){"A"}, 
            TimeActivityStart =new  DateTime(2023,03,13,5,0,0), 
            TimeActivityEnd =  new DateTime(2023,03,16,5,0,0),
            TypeActivityBuild = TypeActivityBuild.Component };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>(){};


            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);
            
            var workerInActivityRule = new WorkerInActivityRule(activityValidatorService,request.Workers,request.TimeActivityStart,request.TimeActivityEnd, Guid.Empty);
            Assert.AreEqual(false,workerInActivityRule.IsBroken());
        }

         [Test]
        public void ShouldValideWorkerInActivity()
        {

            var activityValidatorService = Substitute.For<IActivityValidatorService>();
            var request = new {Workers = new List<string>(){"A"}, 
            TimeActivityStart =new  DateTime(2023,03,13,5,0,0), 
            TimeActivityEnd =  new DateTime(2023,03,16,5,0,0),
            TypeActivityBuild = TypeActivityBuild.Component };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>(){new ActivityWithWorkerDto{WorkerId = "A"}};

            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);
            
            var workerInActivityRule = new WorkerInActivityRule(activityValidatorService,request.Workers,request.TimeActivityStart,request.TimeActivityEnd, Guid.Empty);
            Assert.AreEqual(true,workerInActivityRule.IsBroken());
        }
    }
}