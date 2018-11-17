using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    public class ProgressAverage : ProgressBase
    {
        public decimal? AverageLastWeek
        {
            get
            {
                if (WeightsLastWeek.Any())
                {
                    decimal? average = WeightsLastWeek?.Average();
                    if (average.HasValue)
                    {
                        return Math.Round(average.Value, 2);
                    }
                }
                return null;
            }
        }

        public decimal? Average2WeeksAgo
        {
            get
            {
                if (Weights2WeeksAgo.Any())
                {
                    decimal? average = Weights2WeeksAgo?.Average();
                    if (average.HasValue)
                    {
                        return Math.Round(average.Value, 2);
                    }
                }

                return null;
            }
        }

        public decimal? Average3WeeksAgo
        {
            get
            {
                if (Weights3WeeksAgo.Any())
                {
                    decimal? average = Weights3WeeksAgo?.Average();
                    if (average.HasValue)
                    {
                        return Math.Round(average.Value, 2);
                    }
                }
                return null;
            }
        }

        public decimal? Average4WeeksAgo
        {
            get
            {
                if (Weights4WeeksAgo.Any())
                {
                    decimal? average = Weights4WeeksAgo?.Average();
                    if (average.HasValue)
                    {
                        return Math.Round(average.Value, 2);
                    }
                }
                return null;
            }
        }

        public decimal? AverageDifferenceLastWeek
        {
            get
            {
                if (AverageLastWeek.HasValue && Average2WeeksAgo.HasValue)
                {
                    return AverageLastWeek.Value - Average2WeeksAgo.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? AverageDifference2WeeksAgo
        {
            get
            {
                if (Average2WeeksAgo.HasValue && Average3WeeksAgo.HasValue)
                {
                    return Average2WeeksAgo - Average3WeeksAgo;
                }
                else
                {
                    return null;
                }
            }
        }

        public decimal? AverageDifference3WeeksAgo
        {
            get
            {
                if (Average3WeeksAgo.HasValue && Average4WeeksAgo.HasValue)
                {
                    return Average3WeeksAgo - Average4WeeksAgo;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
