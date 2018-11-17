using deejayvee.WeighIn.Library;
using deejayvee.WeighIn.Library.Model;
using deejayvee.WeighIn.Library.Progress;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Test.Progress
{
    [TestFixture]
    public class GetAverageTEST : TestBase
    {
        [Test]
        public void TestRetrieve()
        {
            ProgressAverage average = new ProgressAverage()
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

            Assert.That(average.AverageLastWeek, Is.EqualTo(108.26m));
            Assert.That(average.Average2WeeksAgo, Is.EqualTo(109.30m));
            Assert.That(average.Average3WeeksAgo, Is.EqualTo(109.67m));
            Assert.That(average.Average4WeeksAgo, Is.EqualTo(110.41m));
            Assert.That(average.AverageDifferenceLastWeek, Is.EqualTo(-1.04m));
            Assert.That(average.AverageDifference2WeeksAgo, Is.EqualTo(-0.37m));
            Assert.That(average.AverageDifference3WeeksAgo, Is.EqualTo(-0.74m));
        }
    }
}
