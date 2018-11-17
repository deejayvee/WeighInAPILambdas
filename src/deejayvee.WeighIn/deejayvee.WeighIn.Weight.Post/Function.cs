using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using deejayvee.WeighIn.Library;
using deejayvee.WeighIn.Library.Weight;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace deejayvee.WeighIn.Weight.Post
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
            context.Logger.Log("Starting User Post call");

            using (AwsFactory factory = new AwsFactory(context.Logger))
            {
                using (PostWeight postWeight = new PostWeight(factory))
                {
                    string userId = request.PathParameters["userId"];
                    string firstName = request.PathParameters["firstName"];
                    string weightDateString = request.PathParameters["weightDate"];
                    string weightString = request.QueryStringParameters["weight"];

                    context.Logger.LogLine($"userId=\"{userId}\"");
                    context.Logger.LogLine($"firstName=\"{firstName}\"");
                    context.Logger.LogLine($"weightDate=\"{weightDateString}\"");
                    context.Logger.LogLine($"weight=\"{weightString}\"");

                    DateTime weightDate = Convert.ToDateTime(weightDateString);
                    decimal weight = Convert.ToDecimal(weightString);

                    string jsonResponse = postWeight.Save(userId, firstName, weightDate, weight);

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
