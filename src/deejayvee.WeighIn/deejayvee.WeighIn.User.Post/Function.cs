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

namespace deejayvee.WeighIn.User.Post
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
                using (PostUser postUser = new PostUser(factory))
                {
                    context.Logger.Log($"Request body: {request.Body}");

                    string jsonResponse = postUser.Save(request.Body);

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
