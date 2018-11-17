using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using deejayvee.WeighIn.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace deejayvee.WeighIn.Library.Progress
{
    public abstract class GetProgressBase : WeighInBase
    {
        protected GetProgressBase(IAwsFactory factory) : base(factory)
        {
        }

        protected void LoadProgress(string userId, string firstName, ProgressBase progress)
        {
            try
            {
                using (IDynamoDBContext context = Factory.DynamoDBContext)
                {
                    Factory.Logger.Log("Loading User");

                    progress.User = context.LoadAsync<WeighInUser>(userId, firstName).Result;

                    if (progress.User==null)
                    {
                        Factory.Logger.Log("User not found");
                        throw new WeighInException($"User \"{userId},{firstName}\" not found");
                    }

                    Factory.Logger.Log("User loaded");

                    DateTime week1Start = DateTime.Today.AddDays(-6);
                    DateTime week1End = DateTime.Today;
                    IEnumerable<object> week1 = (new List<object>() { week1Start, week1End });

                    DateTime week2Start = week1Start.AddDays(-7);
                    DateTime week2End = week1End.AddDays(-7);
                    IEnumerable<object> week2 = (new List<object>() { week2Start, week2End });

                    DateTime week3Start = week2Start.AddDays(-7);
                    DateTime week3End = week2End.AddDays(-7);
                    IEnumerable<object> week3 = (new List<object>() { week3Start, week3End });

                    DateTime week4Start = week3Start.AddDays(-7);
                    DateTime week4End = week3End.AddDays(-7);
                    IEnumerable<object> week4 = (new List<object>() { week4Start, week4End });

                    Factory.Logger.Log("Loading Weights Week 1");

                    progress.WeightsLastWeek = context.QueryAsync<WeighInWeight>(progress.User.UserKey, QueryOperator.Between, week1).GetRemainingAsync().Result.Select(W => W.Weight).ToList();

                    Factory.Logger.Log("Loading Weights Week 2");

                    progress.Weights2WeeksAgo = context.QueryAsync<WeighInWeight>(progress.User.UserKey, QueryOperator.Between, week2).GetRemainingAsync().Result.Select(W => W.Weight).ToList();

                    Factory.Logger.Log("Loading Weights Week 3");

                    progress.Weights3WeeksAgo = context.QueryAsync<WeighInWeight>(progress.User.UserKey, QueryOperator.Between, week3).GetRemainingAsync().Result.Select(W => W.Weight).ToList();

                    Factory.Logger.Log("Loading Weights Week 4");

                    progress.Weights4WeeksAgo = context.QueryAsync<WeighInWeight>(progress.User.UserKey, QueryOperator.Between, week4).GetRemainingAsync().Result.Select(W => W.Weight).ToList();

                    Factory.Logger.Log("4 weeks loaded");
                }
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error getting Progress with userId=\"{userId}\" and firstName=\"{firstName}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
