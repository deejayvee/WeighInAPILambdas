using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    public class ProgressMinimum : ProgressBase
    {
        public decimal MinimumLastWeek
        {
            get
            {
                return WeightsLastWeek.Min();
            }
        }

        public decimal Minimum2WeeksAgo
        {
            get
            {
                return Weights2WeeksAgo.Min();
            }
        }

        public decimal Minimum3WeeksAgo
        {
            get
            {
                return Weights3WeeksAgo.Min();
            }
        }

        public decimal Minimum4WeeksAgo
        {
            get
            {
                return Weights4WeeksAgo.Min();
            }
        }

        public decimal MinimumDifferenceLastWeek
        {
            get
            {
                return MinimumLastWeek - Minimum2WeeksAgo;
            }
        }

        public decimal MinimumDifference2WeeksAgo
        {
            get
            {
                return Minimum2WeeksAgo - Minimum3WeeksAgo;
            }
        }

        public decimal MinimumDifference3WeeksAgo
        {
            get
            {
                return Minimum3WeeksAgo - Minimum4WeeksAgo;
            }
        }
    }
}
