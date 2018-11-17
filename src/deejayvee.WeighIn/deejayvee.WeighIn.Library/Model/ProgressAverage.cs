using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    public class ProgressAverage : ProgressBase
    {
        public decimal AverageLastWeek
        {
            get
            {
                return Math.Round(WeightsLastWeek.Average(), 2);
            }
        }

        public decimal Average2WeeksAgo
        {
            get
            {
                return Math.Round(Weights2WeeksAgo.Average(), 2);
            }
        }

        public decimal Average3WeeksAgo
        {
            get
            {
                return Math.Round(Weights3WeeksAgo.Average(), 2);
            }
        }

        public decimal Average4WeeksAgo
        {
            get
            {
                return Math.Round(Weights4WeeksAgo.Average(), 2);
            }
        }

        public decimal AverageDifferenceLastWeek
        {
            get
            {
                return AverageLastWeek - Average2WeeksAgo;
            }
        }

        public decimal AverageDifference2WeeksAgo
        {
            get
            {
                return Average2WeeksAgo - Average3WeeksAgo;
            }
        }

        public decimal AverageDifference3WeeksAgo
        {
            get
            {
                return Average3WeeksAgo - Average4WeeksAgo;
            }
        }
    }
}
