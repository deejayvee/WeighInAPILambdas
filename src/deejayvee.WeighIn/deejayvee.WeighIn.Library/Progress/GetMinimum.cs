using deejayvee.WeighIn.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.Progress
{
    public class GetMinimum : GetProgressBase
    {
        public GetMinimum(IAwsFactory factory) : base(factory)
        {
        }

        public string Retrieve(string userId, string firstName)
        {
            try
            {
                ProgressMinimum minimum = new ProgressMinimum();
                LoadProgress(userId, firstName, minimum);

                return JsonConvert.SerializeObject(minimum);
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error getting Minimum Progress with userId=\"{userId}\" and firstName=\"{firstName}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
