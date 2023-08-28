using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Activities.Domain.Models.Repositories;
using Activities.Application.DomainServices;
using Activities.Domain.Enums;
using Activities.Domain.Models.Entities;
using Activities.Domain.ValidatorServices;

namespace Activities.Tests.Activits.Validators
{
    public class ActivityValidatorServiceTest
    {
        public FactoryBuildActivity factoryBuildActivity { get; set; }

        [SetUp]
        public void Setup()
        {
            factoryBuildActivity = new FactoryBuildActivity();
            factoryBuildActivity.Setup();
        }



        [Test]
        public void ShouldReturnOneWorkersWithConflits_2()
        {
            var activityValidatorService = new ActivityValidatorService(factoryBuildActivity.ActivityRepository);
            var workers = new List<string>() { "A" };
            var timeActivityStart = new DateTime(2023, 03, 24, 5, 0, 0);
            var timeActivityEnd = new DateTime(2023, 03, 26, 5, 0, 0);
            var workersResult = activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(workers,
                                                                                                       timeActivityStart,
                                                                                                       timeActivityEnd,
                                                                                                       Guid.Empty);
            factoryBuildActivity.CloseConnection();
            Assert.AreEqual(0, workersResult.Count);
        }

        [Test]
        public void ShouldReturnNoOneWorkersWithConflits()
        {


            var activityValidatorService = new ActivityValidatorService(factoryBuildActivity.ActivityRepository);
            var workers = new List<string>() { "A" };
            var timeActivityStart = new DateTime(2020, 02, 12, 5, 0, 0);
            var timeActivityEnd = new DateTime(2022, 02, 13, 5, 0, 0);
            var workersResult = activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(workers,
                                                                                                       timeActivityStart,
                                                                                                       timeActivityEnd,
                                                                                                       Guid.Empty);
            factoryBuildActivity.CloseConnection();

            Assert.AreEqual(0, workersResult.Count);
        }

        [Test]
        public void ShouldReturnTwoWorkersWithConflits()
        {

            var activityValidatorService = new ActivityValidatorService(factoryBuildActivity.ActivityRepository);
            var workers = new List<string>() { "C", "B" };
            var timeActivityStart = new DateTime(2023, 03, 01, 5, 0, 0);
            var timeActivityEnd = new DateTime(2023, 03, 13, 5, 0, 0);
            var workersResult = activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(workers,
                                                                                                       timeActivityStart,
                                                                                                       timeActivityEnd,
                                                                                                       Guid.Empty);
            factoryBuildActivity.CloseConnection();

            Assert.AreEqual(2, workersResult.Count);
        }

        [Test]
        public void ShouldReturnWorkersWithoutConflitsByIgnoreActivityId()
        {
            var activityValidatorService = new ActivityValidatorService(factoryBuildActivity.ActivityRepository);

            var activityTmp = Activity.Create(new List<string>() { "B" },
                        TypeActivityBuild.Machine,
                        new DateTime(2023, 04, 01, 5, 0, 0),
                        new DateTime(2023, 03, 20, 5, 0, 0), activityValidatorService);

            factoryBuildActivity.SqlLiteConfig.ActivityContext.Add(activityTmp);

            factoryBuildActivity.SqlLiteConfig.ActivityContext.SaveChanges();

            var workers = new List<string>() { "B" };
            var timeActivityStart = new DateTime(2023, 04, 05, 5, 0, 0);
            var timeActivityEnd = new DateTime(2023, 04, 07, 5, 0, 0);
            var workersResult = activityValidatorService.GetWorkersInActivityByTimeActivityStartAndEnd(workers,
                                                                                                       timeActivityStart,
                                                                                                       timeActivityEnd,
                                                                                                      activityTmp.Id);
            factoryBuildActivity.CloseConnection();

            Assert.AreEqual(0, workersResult.Count);
        }


    }
}