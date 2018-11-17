using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using deejayvee.WeighIn.Library;
using deejayvee.WeighIn.Library.User;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace deejayvee.WeighIn.User.Get
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            context.Logger.Log("Starting User Get call");

            using (AwsFactory factory = new AwsFactory(context.Logger))
            {
                string userId = request.PathParameters["userId"];
                context.Logger.LogLine($"userId=\"{userId}\"");
                
                string firstName;

                if (request.PathParameters.ContainsKey("firstName"))
                {
                    firstName = request.PathParameters["firstName"];
                }
                else
                {
                    firstName = null;
                }

                context.Logger.LogLine($"firstName=\"{firstName}\"");

                using (GetUser getUser = new GetUser(factory))
                {
                    string jsonResponse = getUser.Retrieve(userId, firstName);

                    context.Logger.LogLine($"Response: {jsonResponse}");
                    APIGatewayProxyResponse response = new APIGatewayProxyResponse()
                    {
                        Body = jsonResponse,
                        StatusCode = 200
                    };

                    return response;
                }
            }
        }
    }
}
