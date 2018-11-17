using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using deejayvee.WeighIn.Library;
using deejayvee.WeighIn.Library.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace deejayvee.WeighIn.Test.Progress
{
    public abstract class ProgressTestBase : TestBase
    {
        protected class Week
        {
            public DateTime StartDate { get; set; }
            public DateTime EndDate { get; set; }
        }

        protected Dictionary<int, Week> Weeks { get; set; } = new Dictionary<int, Week>();
        protected string ActualUserId { get; set; }
        protected string ActualFirstName { get; set; }

        protected IAwsFactory GetAwsFactory()
        {
            Mock<AsyncSearch<WeighInWeight>> searchResults1 = new Mock<AsyncSearch<WeighInWeight>>();
            searchResults1.Setup(r=>r.GetRemainingAsync(It.IsAny<CancellationToken>())).Returns(()=>
            {
                List<WeighInWeight> weights = new List<WeighInWeight>()
                {
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today, Weight = 107.7m},
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-1), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-2), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-3), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-4), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-5), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-6), Weight = 108.3m }
                };

                return Task.FromResult<List<WeighInWeight>>(weights);
            });

            Mock<AsyncSearch<WeighInWeight>> searchResults2 = new Mock<AsyncSearch<WeighInWeight>>();
            searchResults2.Setup(r => r.GetRemainingAsync(It.IsAny<CancellationToken>())).Returns(() =>
            {
                List<WeighInWeight> weights = new List<WeighInWeight>()
                {
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-7), Weight = 107.7m},
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-8), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-9), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-10), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-11), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-12), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-13), Weight = 108.3m }
                };

                return Task.FromResult<List<WeighInWeight>>(weights);
            });

            Mock<AsyncSearch<WeighInWeight>> searchResults3 = new Mock<AsyncSearch<WeighInWeight>>();
            searchResults3.Setup(r => r.GetRemainingAsync(It.IsAny<CancellationToken>())).Returns(() =>
            {
                List<WeighInWeight> weights = new List<WeighInWeight>()
                {
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-14), Weight = 107.7m},
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-15), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-16), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-17), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-18), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-19), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-20), Weight = 108.3m }
                };

                return Task.FromResult<List<WeighInWeight>>(weights);
            });

            Mock<AsyncSearch<WeighInWeight>> searchResults4 = new Mock<AsyncSearch<WeighInWeight>>();
            searchResults4.Setup(r => r.GetRemainingAsync(It.IsAny<CancellationToken>())).Returns(() =>
            {
                List<WeighInWeight> weights = new List<WeighInWeight>()
                {
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-21), Weight = 107.7m},
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-22), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-23), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-24), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-25), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-26), Weight = 108.3m },
                    new WeighInWeight() { UserKey = "TestUserKey", WeightDate = DateTime.Today.AddDays(-27), Weight = 108.3m }
                };

                return Task.FromResult<List<WeighInWeight>>(weights);
            });
            Queue<AsyncSearch<WeighInWeight>> weightQueue = new Queue<AsyncSearch<WeighInWeight>>(new List<AsyncSearch<WeighInWeight>>()
            {
                searchResults1.Object,
                searchResults2.Object,
                searchResults3.Object,
                searchResults4.Object
            });

            int CurrentWeek = 0;

            Mock<IDynamoDBContext> context = new Mock<IDynamoDBContext>();
            context.Setup(C => C.QueryAsync<WeighInWeight>(It.IsAny<string>(), It.IsAny<QueryOperator>(), It.IsAny<IEnumerable<object>>(), It.IsAny<DynamoDBOperationConfig>())).Returns((string key, QueryOperator op, IEnumerable<object> param, DynamoDBOperationConfig config) =>
              {
                  CurrentWeek++;
                  Week week = new Week()
                  {
                      StartDate = (DateTime)param.ElementAt(0),
                      EndDate = (DateTime)param.ElementAt(1)
                  };

                  Weeks.Add(CurrentWeek, week);
                  return weightQueue.Dequeue();
              });

            context.Setup(D => D.LoadAsync<WeighInUser>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns((string a, string b, object c) =>
            {
                ActualUserId = a;
                ActualFirstName = b;
                WeighInUser user = new WeighInUser()
                {
                    UserId = a,
                    FirstName = b,
                    UserKey = "TestKey",
                    StartingWeight = 88.8m,
                    StartingWeightDate = new DateTime(2018, 7, 16)
                };
                return Task.FromResult<WeighInUser>(user);
            });

            TestAwsFactory factory = new TestAwsFactory()
            {
                DynamoDBContext = context.Object
            };

            return factory;
        }
    }
}
