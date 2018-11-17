using Amazon.DynamoDBv2.DataModel;
using deejayvee.WeighIn.Library;
using deejayvee.WeighIn.Library.Model;
using deejayvee.WeighIn.Library.Weight;
using Moq;
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
    public class GetWeightTEST : TestBase
    {
        [Test]
        public void TestRetrieve_Success()
        {
            string actualUserKey = string.Empty;
            DateTime? actualWeightDate = null;

            Mock<IDynamoDBContext> context = new Mock<IDynamoDBContext>();
            context.Setup(D => D.LoadAsync<WeighInWeight>(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<CancellationToken>())).Returns((string a, DateTime b, object c) =>
            {
                actualUserKey = a;
                actualWeightDate = b;
                WeighInWeight weight = new WeighInWeight()
                {
                    UserKey = a,
                    WeightDate = b,
                    Weight = 88.8m
                };

                return Task.FromResult<WeighInWeight>(weight);
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

            using (GetWeight getWeight = new GetWeight(factory))
            {
                string jsonResult = getWeight.Retrieve("UnitTestId","Unit", new DateTime(2018,7,16));

                Console.WriteLine(jsonResult);

                Assert.That(actualUserKey, Is.EqualTo("UnitTestKey"), "actualUserKey");
                Assert.That(actualWeightDate, Is.EqualTo(new DateTime(2018,7,16)), "actualWeightDate");

                dynamic user = JObject.Parse(jsonResult);
                Assert.That((string)user.UserKey, Is.EqualTo("UnitTestKey"), "UserKey");
                Assert.That((DateTime)user.WeightDate, Is.EqualTo(new DateTime(2018,7,16)), "WeightDate");
                Assert.That((decimal)user.Weight, Is.EqualTo(88.8m), "Weight");
            }
        }

        [Test]
        public void TestRetrieve_Fail()
        {
            Mock<IDynamoDBContext> context = new Mock<IDynamoDBContext>();
            context.Setup(D => D.LoadAsync<WeighInWeight>(It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<CancellationToken>())).Returns((string a, DateTime? b, object c) =>
            {
                throw new Exception("Test Exception");
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

            using (GetWeight getWeight = new GetWeight(factory))
            {
                WeighInException wEx = Assert.Throws<WeighInException>(() => getWeight.Retrieve("UnitTestId","Unit", new DateTime(2018,7,16)));
                Assert.That(wEx.Message, Is.EqualTo("Test Exception"));
            }
        }
    }
}
