using System;
using System.Collections.Generic;
using System.Text;
using Amazon.Lambda.Core;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace deejayvee.WeighIn.Library
{
    public class AwsFactory : IAwsFactory
    {
        public AwsFactory(ILambdaLogger logger)
        {
            CurrentLogger = logger;
        }

        private static ILambdaLogger CurrentLogger { get; set; }
        private static IAmazonDynamoDB CurrentDynamoDbClient { get; set; }

        public ILambdaLogger Logger
        {
            get
            {
                return CurrentLogger;
            }
        }

        public IAmazonDynamoDB DynamoDb
        {
            get
            {
                if (CurrentDynamoDbClient == null)
                {
                    CurrentDynamoDbClient = new AmazonDynamoDBClient();
                }

                return CurrentDynamoDbClient;
            }
        }

        public IDynamoDBContext DynamoDBContext
        {
            get
            {
                return new DynamoDBContext(DynamoDb, new DynamoDBContextConfig() { ConsistentRead = true });
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (CurrentDynamoDbClient != null)
            {
                CurrentDynamoDbClient.Dispose();
                CurrentDynamoDbClient = null;
            }
        }
    }
}
