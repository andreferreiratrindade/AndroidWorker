using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Activities.Domain.Enums;
using Activities.Domain.Models.Entities;
using Activities.Domain.Rules;
using Activities.Domain.ValidatorServices;

namespace Activities.Tests.Activits
{
    public class ActivityWithTypeBuildComponentAcceptJustOneWorkerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldValidWorkerWithTypeBuildComponent()
        {

            var workers = new List<string>() { "A" };

            var rule = new ActivityWithTypeBuildComponentAcceptJustOneWorkerRule(TypeActivityBuild.Component,
                                                                                 workers);

            Assert.AreEqual(false, rule.IsBroken());
        }

        [Test]
        public void ShouldInvalidWorkerWithTypeBuildComponent()
        {

            var workers = new List<string>() { "A", "B" };

            var rule = new ActivityWithTypeBuildComponentAcceptJustOneWorkerRule(TypeActivityBuild.Component,
                                                                                 workers);

            Assert.AreEqual(true, rule.IsBroken());
        }

        [Test]
        public void ShouldValidWorkerWithTypeBuildMachineAndTwoWorkers()
        {

            var workers = new List<string>() { "A", "B" };

            var rule = new ActivityWithTypeBuildComponentAcceptJustOneWorkerRule(TypeActivityBuild.Machine,
                                                                                 workers);

            Assert.AreEqual(false, rule.IsBroken());
        }

        [Test]
        public void ShouldValidWorkerWithTypeBuildMachine()
        {

            var workers = new List<string>() { "A" };

            var rule = new ActivityWithTypeBuildComponentAcceptJustOneWorkerRule(TypeActivityBuild.Machine,
                                                                                 workers);

            Assert.AreEqual(false, rule.IsBroken());
        }
    }
}