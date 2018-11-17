using Amazon.DynamoDBv2.DataModel;
using deejayvee.WeighIn.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace deejayvee.WeighIn.Library.Weight
{
    public class PostWeight : WeighInBase
    {
        public PostWeight(IAwsFactory factory) : base(factory)
        {
        }

        public string Save(string userId, string firstName, DateTime weightDate, decimal weight)
        {
            try
            {
                Factory.Logger.Log($"Using User Id: {userId}");
                Factory.Logger.Log($"Using Weight Date: {weightDate}");
                Factory.Logger.Log($"Using Weight: {weight}");

                using (IDynamoDBContext context = Factory.DynamoDBContext)
                {
                    WeighInUser user = context.LoadAsync<WeighInUser>(userId, firstName).Result;

                    if (user == null)
                    {
                        throw new WeighInException($"User \"{userId},{firstName}\" not found");
                    }

                    Factory.Logger.Log($"Saving weight for \"{user.UserKey}\"");
                    WeighInWeight weighInWeight = new WeighInWeight()
                    {
                        UserKey = user.UserKey,
                        WeightDate = weightDate,
                        Weight = weight
                    };
                    Task t = context.SaveAsync<WeighInWeight>(weighInWeight);
                    t.Wait();
                    if (t.Status== TaskStatus.Faulted)
                    {
                        throw t.Exception;
                    }
                    Factory.Logger.Log($"Weight saved for \"{user.UserKey}\"");

                    return JsonConvert.SerializeObject(weighInWeight);
                }                
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error saving WeighInWeight with userId=\"{userId}\" and firstName=\"{firstName}\" and weightDate=\"{weightDate}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
