using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    [DynamoDBTable("WeighIn_Weight")]
    public class WeighInWeight
    {
        [DynamoDBProperty(AttributeName = "userKey")]
        [DynamoDBHashKey]
        public string UserKey { get; set; }
        [DynamoDBProperty(AttributeName = "weightDate")]
        [DynamoDBRangeKey]
        public DateTime? WeightDate { get; set; }
        [DynamoDBProperty(AttributeName = "weight")]
        public decimal Weight { get; set; }
    }
}
