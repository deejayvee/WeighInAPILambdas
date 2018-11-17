using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;

namespace deejayvee.WeighIn.Library.Model.Converters
{
    public class DateTimeConverter : IPropertyConverter
    {
        public object FromEntry(DynamoDBEntry entry)
        {
            if(DynamoDBEntryConversion.V2.TryConvertFromEntry(entry, out long dateTimeNumber))
            {
                string dateTimeString = dateTimeNumber.ToString();
                if (dateTimeString.Length!=8)
                {
                    return (DateTime?)null;
                }
                return new DateTime(int.Parse(dateTimeString.Substring(0, 4)),
                                                 int.Parse(dateTimeString.Substring(4, 2)),
                                                 int.Parse(dateTimeString.Substring(6, 2)));
            }
            else
            {
                return (DateTime?)null;
            }
        }

        public DynamoDBEntry ToEntry(object value)
        {
            DateTime? dateTimeValue = value as DateTime?;
            if (dateTimeValue != null)
            {
                return new Primitive(dateTimeValue.Value.ToString("yyyyMMdd"), true);
            }
            else
            {
                return null;
            }
        }
    }
}
