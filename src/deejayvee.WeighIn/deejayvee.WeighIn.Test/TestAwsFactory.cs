using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using deejayvee.WeighIn.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Test
{
    public class TestAwsFactory : IAwsFactory
    {
        private ILambdaLogger CurrentLogger { get; set; }

        public IAmazonDynamoDB DynamoDb { get; set; }

        public IDynamoDBContext DynamoDBContext { get; set; }

        public ILambdaLogger Logger
        {
            get
            {
                if (CurrentLogger == null)
                {
                    CurrentLogger = new TestLogger();
                }

                return CurrentLogger;
            }
        }

        

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (DynamoDb != null)
            {
                DynamoDb.Dispose();
                DynamoDb = null;
            }
        }
    }
}
