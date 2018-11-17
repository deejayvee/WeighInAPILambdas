using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using deejayvee.WeighIn.Library;
using deejayvee.WeighIn.Library.Model;
using deejayvee.WeighIn.Library.User;
using Moq;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace deejayvee.WeighIn.Test.User
{
    [TestFixture]
    public class GetUserTEST : TestBase
    {
        [Test]
        public void TestRetrieve_Success()
        {
            string actualUserId = string.Empty;
            string actualFirstName = string.Empty;

            Mock<IDynamoDBContext> context = new Mock<IDynamoDBContext>();
            context.Setup(D => D.LoadAsync<WeighInUser>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns((string a, string b, object c) =>
              {
                  actualUserId = a;
                  actualFirstName = b;
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

            using (GetUser getUser = new GetUser(factory))
            {
                string jsonResult = getUser.Retrieve("UnitTestId", "Unit");

                Console.WriteLine(jsonResult);

                Assert.That(actualUserId, Is.EqualTo("UnitTestId"), "actualUserId");
                Assert.That(actualFirstName, Is.EqualTo("Unit"), "actualFirstName");

                dynamic user = JObject.Parse(jsonResult);
                Assert.That((string)user.UserId, Is.EqualTo("UnitTestId"), "UserId");
                Assert.That((string)user.FirstName, Is.EqualTo("Unit"), "FirstName");
                Assert.That((string)user.UserKey, Is.EqualTo("TestKey"), "UserKey");
                Assert.That((decimal)user.StartingWeight, Is.EqualTo(88.8m), "StartingWeight");
                Assert.That((DateTime)user.StartingWeightDate, Is.EqualTo(new DateTime(2018, 7, 16)), "StartingWeightDate");
            }
        }

        [Test]
        public void TestRetrieve_Fail()
        {
            Mock<IDynamoDBContext> context = new Mock<IDynamoDBContext>();
            context.Setup(D => D.LoadAsync<WeighInUser>(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns((string a, string b, object c) =>
            {
                throw new Exception("Test Exception");
            });

            TestAwsFactory factory = new TestAwsFactory()
            {
                DynamoDBContext = context.Object
            };

            using (GetUser getUser = new GetUser(factory))
            {
                WeighInException wEx = Assert.Throws<WeighInException>(() => getUser.Retrieve("UnitTestId", "Unit"));
                Assert.That(wEx.Message, Is.EqualTo("Test Exception"));
            }
        }
    }
}
