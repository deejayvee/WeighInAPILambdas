using deejayvee.WeighIn.Library.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Test.Progress
{
    [TestFixture]
    public class GetTotalTEST : TestBase
    {
        [Test]
        public void TestRetrieve()
        {
            ProgressTotal total = new ProgressTotal()
            {
                WeightsLastWeek = new List<decimal>()
                {
                    108.5m,
                    108.1m,
                    108.5m,
                    108.4m,
                    108.3m,
                    108.3m,
                    107.7m
                },
                Weights2WeeksAgo = new List<decimal>()
                {
                    109.3m,
                    109.5m,
                    109.2m,
                    109.5m,
                    109.4m,
                    109.4m,
                    108.8m
                },
                Weights3WeeksAgo = new List<decimal>()
                {
                    109.7m,
                    110.3m,
                    109.7m,
                    109.9m,
                    109.4m,
                    109.3m,
                    109.4m
                },
                Weights4WeeksAgo = new List<decimal>()
                {
                    110.8m,
                    110.3m,
                    110.7m,
                    111.0m,
                    110.4m,
                    109.9m,
                    109.8m
                },
                User = new WeighInUser()
                {
                    UserId = "TestUserId",
                    FirstName = "Unit",
                    UserKey = "TestUserKey",
                    StartingWeight = 124m,
                    StartingWeightDate = new DateTime(2018, 6, 15)
                }
            };

            Assert.That(total.TotalWeightLoss, Is.EqualTo(16.3m));
            Assert.That(total.TotalPeriod.Days, Is.EqualTo(155));
        }
    }
}
