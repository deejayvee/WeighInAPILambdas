using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using System;

namespace deejayvee.WeighIn.Library
{
    public interface IAwsFactory : IDisposable
    {
        IAmazonDynamoDB DynamoDb { get; }
        IDynamoDBContext DynamoDBContext { get; }
        ILambdaLogger Logger { get; }
        
    }
}