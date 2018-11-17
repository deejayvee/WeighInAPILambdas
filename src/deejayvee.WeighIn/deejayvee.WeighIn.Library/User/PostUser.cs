using Amazon.DynamoDBv2.DataModel;
using deejayvee.WeighIn.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace deejayvee.WeighIn.Library.User
{
    public class PostUser : WeighInBase
    {
        public PostUser(IAwsFactory factory) : base(factory)
        {
        }

        public string Save(string userJson)
        {
            try
            {
                WeighInUser user = JsonConvert.DeserializeObject<WeighInUser>(userJson);
                Factory.Logger.Log($"User \"{user.UserId},{user.FirstName}\" deserialized");

                if (string.IsNullOrEmpty(user.UserKey))
                {
                    Factory.Logger.Log($"User does not have a user key");
                    user.UserKey = Guid.NewGuid().ToString();
                }

                Factory.Logger.Log($"Using User ID: {user.UserId}");
                Factory.Logger.Log($"Using Firstname: {user.FirstName}");
                Factory.Logger.Log($"Using User Key: {user.UserKey}");
                Factory.Logger.Log($"Using Weight Date: {user.StartingWeightDate}");
                Factory.Logger.Log($"Using Weight: {user.StartingWeight}");

                user.LastUseDateTime = DateTime.Now;

                using (IDynamoDBContext context = Factory.DynamoDBContext)
                {
                    Factory.Logger.Log($"Saving user \"{user.UserId},{user.FirstName}\"");
                    Task t = context.SaveAsync<WeighInUser>(user);
                    t.Wait();
                    if (t.Status == TaskStatus.Faulted)
                    {
                        throw t.Exception;
                    }
                    Factory.Logger.Log($"User saved \"{user.UserId},{user.FirstName}\"");
                }

                return JsonConvert.SerializeObject(user);
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error saving WeighInUser with json=\"{userJson}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
