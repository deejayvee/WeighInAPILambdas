using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    public abstract class ProgressBase
    {
        [JsonIgnore]
        public WeighInUser User { get; set; }
        public List<decimal> WeightsLastWeek { get; set; }
        public List<decimal> Weights2WeeksAgo { get; set; }
        public List<decimal> Weights3WeeksAgo { get; set; }
        public List<decimal> Weights4WeeksAgo { get; set; }
    }
}
