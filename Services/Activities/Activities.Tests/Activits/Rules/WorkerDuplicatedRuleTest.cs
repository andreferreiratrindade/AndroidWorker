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
    public class WorkerDuplicatedRuleTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ShouldReturnWorkerDuplicated()
        {

            var workers = new List<string>(){"A", "B", "C", "D", "B"}; 

            var rule = new WorkerDuplicatedRule(workers);

            Assert.AreEqual(true, rule.IsBroken());
        }

           [Test]
        public void ShouldReturnWorkkerNotDuplicated()
        {

            var workers = new List<string>(){"A", "B", "C", "E", "F"}; 

            var rule = new WorkerDuplicatedRule(workers);

            Assert.AreEqual(false, rule.IsBroken());
        }
    }
}