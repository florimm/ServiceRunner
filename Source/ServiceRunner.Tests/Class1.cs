using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Contracts;
using NUnit.Framework;

namespace ServiceRunner.Tests
{
    [TestFixture]
    public class SchedulerTests
    {
        [Test]
        public void TestSchedulerForTheFirstTime()
        {
            //var sc = new Scheduler(1,ExecuteType.Min);
            //Assert.IsTrue(sc.CanBeExecuted());
            ///Assert.IsFalse(sc.CanBeExecuted());
        }

        [Test]
        public void TestSchedulerAfterOneMinut()
        {
            //var sc = new Scheduler(1, ExecuteType.Min);
            //Assert.IsTrue(sc.CanBeExecuted());
            //sc.LastExecuted = sc.LastExecuted.Value.AddMinutes(1);
            //Assert.IsTrue(sc.CanBeExecuted());
        }
    }
}
