using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    public class ProgressTotal : ProgressBase
    {
        public decimal? TotalWeightLoss
        {
            get
            {
                if (!WeightsLastWeek.Any() || WeightsLastWeek?.Min()==null || User==null || !User.StartingWeight.HasValue)
                {
                    return null;
                }
                return Math.Abs(WeightsLastWeek.Min() - User.StartingWeight.Value);
            }
        }

        public TimeSpan? TotalPeriod
        {
            get
            {
                if (User == null || !User.StartingWeight.HasValue)
                {
                    return null;
                }
                return DateTime.Today.Subtract(User.StartingWeightDate.Value);
            }
        }
    }
}
