using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Framework.Core.DomainObjects;
using NSubstitute;
using NUnit.Framework;
using Activities.Domain.DTO;
using Activities.Domain.Enums;
using Activities.Domain.Models.Entities;
using Activities.Domain.ValidatorServices;

namespace Activities.Tests.Activits
{
    public class AddActivityTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShoutReturnActivityWithEventCreated()
        {
            var activityValidatorService = Substitute.For<IActivityValidatorService>();

            var request = new
            {
                Workers = new List<string>() { "A" },
                TimeActivityStart = new DateTime(2023, 03, 01, 5, 0, 0),
                TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0),
                TypeActivityBuild = TypeActivityBuild.Component
            };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>() { };

            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);

            var activity = Activity.Create(request.Workers,
                                    request.TypeActivityBuild,
                                    request.TimeActivityStart,
                                    request.TimeActivityEnd, activityValidatorService);

            Assert.AreEqual(1, activity.Notificacoes.Count);
            Assert.AreEqual("ActivityCreatedEvent", activity.Notificacoes.First().MessageType);
        }


        [Test]
        public void ShoutReturnActivityWithTimeRestEndCalculated_TypeActivityBuild_Component()
        {
            var activityValidatorService = Substitute.For<IActivityValidatorService>();

            var request = new
            {
                Workers = new List<string>() { "A" },
                TimeActivityStart = new DateTime(2023, 03, 01, 5, 0, 0),
                TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0),
                TypeActivityBuild = TypeActivityBuild.Component
            };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>() { };

            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);

            var activity = Activity.Create(request.Workers,
                                    request.TypeActivityBuild,
                                    request.TimeActivityStart,
                                    request.TimeActivityEnd, activityValidatorService);
            Assert.AreEqual(new DateTime(2023, 03, 05, 7, 0, 0), activity.TimeRestEnd);
        }

        [Test]
        public void ShoutReturnActivityWithTimeRestEndCalculated_TypeActivityBuild_Machine()
        {
            var activityValidatorService = Substitute.For<IActivityValidatorService>();

            var request = new
            {
                Workers = new List<string>() { "A" },
                TimeActivityStart = new DateTime(2023, 03, 01, 5, 0, 0),
                TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0),
                TypeActivityBuild = TypeActivityBuild.Machine
            };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>() { };

            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);

            var activity = Activity.Create(request.Workers,
                                    request.TypeActivityBuild,
                                    request.TimeActivityStart,
                                    request.TimeActivityEnd, activityValidatorService);
            Assert.AreEqual(new DateTime(2023, 03, 05, 9, 0, 0), activity.TimeRestEnd);
        }

        [Test]
        public void ShouldReturnDateTimeRestEndCalculated_TypeActivityBuild_Component()
        {

            var request = new { TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0) };

            var timeRestEnd = Activity.CalculateTimeRest(TypeActivityBuild.Component, request.TimeActivityEnd);

            Assert.AreEqual(new DateTime(2023, 03, 05, 7, 0, 0), timeRestEnd);
        }


        [Test]
        public void ShouldReturnDateTimeRestEndCalculated_TypeActivityBuild_Machine()
        {

            var request = new { TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0) };

            var timeRestEnd = Activity.CalculateTimeRest(TypeActivityBuild.Machine, request.TimeActivityEnd);

            Assert.AreEqual(new DateTime(2023, 03, 05, 9, 0, 0), timeRestEnd);
        }
        [Test]
        public void ShouldCheckInvalidTypeActivityBuild_Machine()
        {
            var activityValidatorService = Substitute.For<IActivityValidatorService>();

            var request = new
            {
                Workers = new List<string>() { "A", "B" },
                TimeActivityStart = new DateTime(2023, 03, 01, 5, 0, 0),
                TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0),
                TypeActivityBuild = TypeActivityBuild.Component
            };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>() { };

            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);

            try
            {
                var activity = Activity.Create(request.Workers,
                                        request.TypeActivityBuild,
                                        request.TimeActivityStart,
                                        request.TimeActivityEnd, activityValidatorService);
            }
            catch (DomainException ex)
            {
                Assert.AreEqual("The TypeActivityBuild equals 'Component' accept just one Worker", ex.Messagens[0]);

            }
        }

        [Test(Description = "Test description")]
        public void ShouldCheckInvalidWorkerDuplicated()
        {
            var activityValidatorService = Substitute.For<IActivityValidatorService>();

            var request = new
            {
                Workers = new List<string>() { "A", "B", "B" },
                TimeActivityStart = new DateTime(2023, 03, 01, 5, 0, 0),
                TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0),
                TypeActivityBuild = TypeActivityBuild.Component
            };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>() { };

            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);

            try
            {
                var activity = Activity.Create(request.Workers,
                                        request.TypeActivityBuild,
                                        request.TimeActivityStart,
                                        request.TimeActivityEnd, activityValidatorService);
            }
            catch (DomainException ex)
            {
                Assert.AreEqual("The workes are duplicated: B", ex.Messagens[0]);

            }
        }

        [Test]
        public void ShouldUpdateTimeStartAndTimeEnd()
        {
            var activityValidatorService = Substitute.For<IActivityValidatorService>();

            var request = new
            {
                Workers = new List<string>() { "A" },
                TimeActivityStart = new DateTime(2023, 03, 01, 5, 0, 0),
                TimeActivityEnd = new DateTime(2023, 03, 05, 5, 0, 0),
                TypeActivityBuild = TypeActivityBuild.Component
            };

            var lstReturnWorkers = new List<ActivityWithWorkerDto>() { };

            activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(request.Workers,
                                                                                   request.TimeActivityStart,
                                                                                   request.TimeActivityEnd, Guid.Empty).Returns(lstReturnWorkers);


            var activity = Activity.Create(request.Workers,
                                    request.TypeActivityBuild,
                                    request.TimeActivityStart,
                                    request.TimeActivityEnd, activityValidatorService);
            var timeStart = new DateTime(2023, 5, 1, 5, 0, 0);
            var timeEnd = new DateTime(2023, 5, 1, 5, 0, 0);
            activity.UpdateTimeStartAndTimeEnd(timeStart, timeEnd, activityValidatorService);
            Assert.AreEqual(new DateTime(2023, 5, 1, 7, 0, 0), activity.TimeRestEnd);
        }
    }
}