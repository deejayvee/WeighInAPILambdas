using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace deejayvee.WeighIn.Library.Model.Converters
{
    public class DecimalConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            throw new NotImplementedException();
        }

        public DynamoDBEntry ToEntry(object value)
        {
            throw new NotImplementedException();
        }
    }
}
