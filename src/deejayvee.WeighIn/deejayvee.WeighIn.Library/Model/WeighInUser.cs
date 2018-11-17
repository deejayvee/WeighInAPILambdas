using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.Model
{
    [DynamoDBTable("WeighIn_User")]
    public class WeighInUser
    {
        [DynamoDBProperty(AttributeName = "userId")]
        [DynamoDBHashKey]
        public string UserId { get; set; }
        [DynamoDBProperty(AttributeName = "firstName")]
        [DynamoDBRangeKey]
        public string FirstName { get; set; }
        [DynamoDBProperty(AttributeName = "userKey")]
        public string UserKey { get; set; }
        [DynamoDBProperty(AttributeName = "startingWeight")]
        public decimal StartingWeight { get; set; }
        [DynamoDBProperty(AttributeName = "startingWeightDate")]
        public DateTime StartingWeightDate { get; set; }
    }
}
