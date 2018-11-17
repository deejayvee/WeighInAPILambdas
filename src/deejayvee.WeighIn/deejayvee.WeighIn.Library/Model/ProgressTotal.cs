using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    public class ProgressTotal : ProgressBase
    {
        public decimal TotalWeightLoss
        {
            get
            {
                return Math.Abs(WeightsLastWeek.Min() - User.StartingWeight);
            }
        }

        public TimeSpan TotalPeriod
        {
            get
            {
                return DateTime.Today.Subtract(User.StartingWeightDate);
            }
        }
    }
}
