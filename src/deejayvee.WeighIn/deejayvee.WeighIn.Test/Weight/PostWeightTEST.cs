using Amazon.DynamoDBv2.DataModel;
using deejayvee.WeighIn.Library.Model;
using deejayvee.WeighIn.Library.Weight;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace deejayvee.WeighIn.Test.Weight
{
    [TestFixture]
    public class PostWeightTEST : TestBase
    {
        [Test]
        public void TestSave()
        {
            WeighInWeight actualWeight = null;

            Mock<IDynamoDBContext> context = new Mock<IDynamoDBContext>();
            context.Setup(C => C.SaveAsync<WeighInWeight>(It.IsAny<WeighInWeight>(), It.IsAny<CancellationToken>())).Returns((WeighInWeight weight, object t) =>
            {
                actualWeight = weight;
                return Task.CompletedTask;
            });
            context.Setup(D => D.LoadAsync<WeighInUser>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns((string a, string b, object c) =>
            {
                WeighInUser user = new WeighInUser()
                {
                    UserId = a,
                    FirstName = b,
                    UserKey = "UnitTestKey",
                    StartingWeight = 88.8m,
                    StartingWeightDate = new DateTime(2018, 7, 16)
                };
                return Task.FromResult<WeighInUser>(user);
            });

            TestAwsFactory factory = new TestAwsFactory()
            {
                DynamoDBContext = context.Object
            };

            using (PostWeight postWeight = new PostWeight(factory))
            {
                string weightOutputJson = postWeight.Save("UnitTestId", "Unit", DateTime.Today, 78.8m);

                Assert.That(actualWeight, Is.Not.Null, "actualUser");
                Assert.That(actualWeight.UserKey, Is.Not.Null.And.Not.Empty, "actualUserKey");
                Assert.That(actualWeight.Weight, Is.EqualTo(78.8m), "actualWeight");
                Assert.That(actualWeight.WeightDate, Is.EqualTo(DateTime.Today), "actualWeightDate");


                dynamic weight = JObject.Parse(weightOutputJson);
                Assert.That((string)weight.UserKey, Is.Not.Null.And.Not.Empty, "UserKey");
                Assert.That((decimal)weight.Weight, Is.EqualTo(78.8m), "StartingWeight");
                Assert.That((DateTime)weight.WeightDate, Is.EqualTo(DateTime.Today), "StartingWeightDate");
                
            }
        }
    }
}
