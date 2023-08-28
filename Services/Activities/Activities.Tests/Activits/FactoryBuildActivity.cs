using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Activities.Domain.Models.Repositories;
using Activities.Application.DomainServices;
using Activities.Domain.Enums;
using Activities.Domain.Models.Entities;
using Activities.Domain.ValidatorServices;
using Activities.Infra.Data.Repository;

namespace Activities.Tests.Activits
{
    public class FactoryBuildActivity
    {
        public SQliteConfig SqlLiteConfig { get; set; }
        public IActivityRepository ActivityRepository { get; set; }
        public void Setup()
        {
            SqlLiteConfig = new SQliteConfig();
            SqlLiteConfig.OpenConnection();
            ActivityRepository = new ActivityRepository(SqlLiteConfig.ActivityContext);

            BuildActivits();


        }
        private void BuildActivits()
        {
            var activityValidatorService = Substitute.For<IActivityValidatorService>();
            ActivityRepository.Add(Activity.Create(new List<string>() { "A" },
                               TypeActivityBuild.Component,
                               new DateTime(2000, 03, 01, 5, 0, 0),
                               new DateTime(2001, 03, 05, 5, 0, 0),
                       activityValidatorService));

            ActivityRepository.Add(Activity.Create(new List<string>() { "B" },
                                    TypeActivityBuild.Component,
                                    new DateTime(2000, 03, 02, 5, 0, 0),
                                    new DateTime(2003, 03, 05, 5, 0, 0),
                                activityValidatorService));

            ActivityRepository.Add(Activity.Create(new List<string>() { "C", "D" },
                            TypeActivityBuild.Machine,
                            new DateTime(2000, 03, 01, 5, 0, 0),
                            new DateTime(2004, 03, 05, 5, 0, 0),
                            activityValidatorService));


            ActivityRepository.Add(Activity.Create(new List<string>() { "A" },
                                    TypeActivityBuild.Component,
                                    new DateTime(2023, 03, 01, 5, 0, 0),
                                    new DateTime(2023, 03, 05, 5, 0, 0),
                            activityValidatorService));

            ActivityRepository.Add(Activity.Create(new List<string>() { "A", "B", "C" },
                                    TypeActivityBuild.Machine,
                                    new DateTime(2023, 03, 10, 5, 0, 0),
                                    new DateTime(2023, 03, 19, 5, 0, 0),
                            activityValidatorService));

            ActivityRepository.Add(Activity.Create(new List<string>() { "B", "C" },
                        TypeActivityBuild.Machine,
                        new DateTime(2023, 03, 25, 5, 0, 0),
                        new DateTime(2023, 03, 30, 5, 0, 0),
                activityValidatorService));
            ActivityRepository.UnitOfWork.Commit();
        }

        public void CloseConnection()
        {
            SqlLiteConfig.CloseConnection();
        }

    }
}