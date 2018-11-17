using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    public class ProgressMinimum : ProgressBase
    {
        public decimal? MinimumLastWeek
        {
            get
            {
                if (WeightsLastWeek.Any())
                {
                    decimal? minimum = WeightsLastWeek?.Min();
                    if (minimum.HasValue)
                    {
                        return Math.Round(minimum.Value, 2);
                    }
                }
                return null;
            }
        }

        public decimal? Minimum2WeeksAgo
        {
            get
            {
                if (Weights2WeeksAgo.Any())
                {
                    decimal? minimum = Weights2WeeksAgo?.Min();
                    if (minimum.HasValue)
                    {
                        return Math.Round(minimum.Value, 2);
                    }
                }
                return null;
            }
        }

        public decimal? Minimum3WeeksAgo
        {
            get
            {
                if (Weights3WeeksAgo.Any())
                {
                    decimal? minimum = Weights3WeeksAgo?.Min();
                    if (minimum.HasValue)
                    {
                        return Math.Round(minimum.Value, 2);
                    }
                }
                return null;
            }
        }

        public decimal? Minimum4WeeksAgo
        {
            get
            {
                if (Weights4WeeksAgo.Any())
                {
                    decimal? minimum = Weights4WeeksAgo?.Min();
                    if (minimum.HasValue)
                    {
                        return Math.Round(minimum.Value, 2);
                    }
                }
                return null;
            }
        }

        public decimal? MinimumDifferenceLastWeek
        {
            get
            {
                if (MinimumLastWeek.HasValue && Minimum2WeeksAgo.HasValue)
                {
                    return MinimumLastWeek.Value - Minimum2WeeksAgo.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? MinimumDifference2WeeksAgo
        {
            get
            {
                if (Minimum2WeeksAgo.HasValue && Minimum3WeeksAgo.HasValue)
                {
                    return Minimum2WeeksAgo - Minimum3WeeksAgo;
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? MinimumDifference3WeeksAgo
        {
            get
            {
                if (Minimum3WeeksAgo.HasValue && Minimum4WeeksAgo.HasValue)
                {
                    return Minimum3WeeksAgo - Minimum4WeeksAgo;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
