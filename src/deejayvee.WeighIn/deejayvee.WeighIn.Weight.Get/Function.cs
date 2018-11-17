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

namespace deejayvee.WeighIn.Weight.Get
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
            context.Logger.Log("Starting Weight Get call");

            using (AwsFactory factory = new AwsFactory(context.Logger))
            {
                string userId = request.PathParameters["userId"];
                string firstName = request.PathParameters["firstName"];
                string weightDateString = request.PathParameters["weightDate"];

                context.Logger.LogLine($"userId=\"{userId}\"");
                context.Logger.LogLine($"firstName=\"{firstName}\"");
                context.Logger.LogLine($"weightDate=\"{weightDateString}\"");

                DateTime weightDate = Convert.ToDateTime(weightDateString);

                using (GetWeight getWeight = new GetWeight(factory))
                {
                    string jsonResponse = getWeight.Retrieve(userId, firstName, weightDate);

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
