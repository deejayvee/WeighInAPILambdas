using deejayvee.WeighIn.Library.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Test.Progress
{
    [TestFixture]
    public class GetMinimumTEST : TestBase
    {
        [Test]
        public void TestRetrieve_FullValues()
        {
            ProgressMinimum minimum = new ProgressMinimum()
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
                }
            };

            Assert.That(minimum.MinimumLastWeek, Is.EqualTo(107.7m));
            Assert.That(minimum.Minimum2WeeksAgo, Is.EqualTo(108.8m));
            Assert.That(minimum.Minimum3WeeksAgo, Is.EqualTo(109.3m));
            Assert.That(minimum.Minimum4WeeksAgo, Is.EqualTo(109.8m));
            Assert.That(minimum.MinimumDifferenceLastWeek, Is.EqualTo(-1.1m));
            Assert.That(minimum.MinimumDifference2WeeksAgo, Is.EqualTo(-0.5m));
            Assert.That(minimum.MinimumDifference3WeeksAgo, Is.EqualTo(-0.5m));
        }

        [Test]
        public void TestRetrieve_Only1Week()
        {
            ProgressMinimum minimum = new ProgressMinimum()
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
                Weights2WeeksAgo = new List<decimal>(),
                Weights3WeeksAgo = new List<decimal>(),
                Weights4WeeksAgo = new List<decimal>()
            };

            Assert.That(minimum.MinimumLastWeek, Is.EqualTo(107.7m));
            Assert.That(minimum.Minimum2WeeksAgo, Is.Null);
            Assert.That(minimum.Minimum3WeeksAgo, Is.Null);
            Assert.That(minimum.Minimum4WeeksAgo, Is.Null);
            Assert.That(minimum.MinimumDifferenceLastWeek, Is.Null);
            Assert.That(minimum.MinimumDifference2WeeksAgo, Is.Null);
            Assert.That(minimum.MinimumDifference3WeeksAgo, Is.Null);
        }

        [Test]
        public void TestRetrieve_NoValues()
        {
            ProgressMinimum minimum = new ProgressMinimum()
            {
                WeightsLastWeek = new List<decimal>(),
                Weights2WeeksAgo = new List<decimal>(),
                Weights3WeeksAgo = new List<decimal>(),
                Weights4WeeksAgo = new List<decimal>()
            };

            Assert.That(minimum.MinimumLastWeek, Is.Null);
            Assert.That(minimum.Minimum2WeeksAgo, Is.Null);
            Assert.That(minimum.Minimum3WeeksAgo, Is.Null);
            Assert.That(minimum.Minimum4WeeksAgo, Is.Null);
            Assert.That(minimum.MinimumDifferenceLastWeek, Is.Null);
            Assert.That(minimum.MinimumDifference2WeeksAgo, Is.Null);
            Assert.That(minimum.MinimumDifference3WeeksAgo, Is.Null);
        }
    }
}
