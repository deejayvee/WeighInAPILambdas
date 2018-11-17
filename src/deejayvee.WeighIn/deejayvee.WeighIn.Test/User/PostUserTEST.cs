using Amazon.DynamoDBv2.DataModel;
using deejayvee.WeighIn.Library.Model;
using deejayvee.WeighIn.Library.User;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace deejayvee.WeighIn.Test.User
{
    [TestFixture]
    public class PostUserTEST : TestBase
    {
        [Test]
        public void TestSave()
        {
            WeighInUser actualUser = null;

            Mock<IDynamoDBContext> context = new Mock<IDynamoDBContext>();
            context.Setup(C => C.SaveAsync<WeighInUser>(It.IsAny<WeighInUser>(), It.IsAny<CancellationToken>())).Returns((WeighInUser user, object t) =>
              {
                  actualUser = user;
                  return Task.CompletedTask;
              });

            TestAwsFactory factory = new TestAwsFactory()
            {
                DynamoDBContext = context.Object
            };

            WeighInUser testUser = new WeighInUser()
            {
                UserId = "TestUserId",
                FirstName = "Unit",
                StartingWeight = 88.8m,
                StartingWeightDate = new DateTime(2018,7,16)
            };
            string testUserJson = JsonConvert.SerializeObject(testUser);

            using (PostUser postUser = new PostUser(factory))
            {
                string userOutputJson = postUser.Save(testUserJson);

                Assert.That(actualUser, Is.Not.Null, "actualUser");
                Assert.That(actualUser.UserId, Is.EqualTo("TestUserId"), "actualUserId");
                Assert.That(actualUser.FirstName, Is.EqualTo("Unit"), "actualFirstName");
                Assert.That(actualUser.StartingWeight, Is.EqualTo(88.8m), "actualStartingWeight");
                Assert.That(actualUser.StartingWeightDate, Is.EqualTo(new DateTime(2018, 7, 16)), "actualStartingWeightDate");
                Assert.That(actualUser.UserKey, Is.Not.Null.And.Not.Empty, "actualUserKey");


                dynamic user = JObject.Parse(userOutputJson);
                Assert.That((string)user.UserId, Is.EqualTo("TestUserId"), "UserId");
                Assert.That((string)user.FirstName, Is.EqualTo("Unit"), "FirstName");
                Assert.That((decimal)user.StartingWeight, Is.EqualTo(88.8m), "StartingWeight");
                Assert.That((DateTime)user.StartingWeightDate, Is.EqualTo(new DateTime(2018, 7, 16)), "StartingWeightDate");
                Assert.That((string)user.UserKey, Is.Not.Null.And.Not.Empty, "UserKey");
            }
        }
    }
}
