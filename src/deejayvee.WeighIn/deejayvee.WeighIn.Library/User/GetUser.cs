using Amazon.DynamoDBv2.DataModel;
using deejayvee.WeighIn.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.User
{
    public class GetUser : WeighInBase
    {
        public GetUser(IAwsFactory factory) : base(factory)
        {
        }

        public string Retrieve(string userId, string firstName)
        {
            try
            {
                using (IDynamoDBContext context = Factory.DynamoDBContext)
                {
                    WeighInUser user = context.LoadAsync<WeighInUser>(userId, firstName).Result;

                    return JsonConvert.SerializeObject(user);
                }
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error getting WeighInUser with userId=\"{userId}\" and firstName=\"{firstName}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
