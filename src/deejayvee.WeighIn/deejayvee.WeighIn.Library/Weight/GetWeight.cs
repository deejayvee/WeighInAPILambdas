using Amazon.DynamoDBv2.DataModel;
using deejayvee.WeighIn.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.Weight
{
    public class GetWeight : WeighInBase
    {
        public GetWeight(IAwsFactory factory) : base(factory)
        {
        }

        public string Retrieve(string userId, string firstName, DateTime weightDate)
        {
            try
            {
               using (IDynamoDBContext context = Factory.DynamoDBContext)
                {
                    WeighInUser user = context.LoadAsync<WeighInUser>(userId, firstName).Result;

                    if (user==null)
                    {
                        throw new WeighInException($"User \"{userId},{firstName}\" not found");
                    }

                    WeighInWeight weight = context.LoadAsync<WeighInWeight>(user.UserKey, weightDate).Result;

                    return JsonConvert.SerializeObject(weight);
                }
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error getting WeighInWeight with userId=\"{userId}\" and firstName=\"{firstName}\" and weightDate=\"{weightDate}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
