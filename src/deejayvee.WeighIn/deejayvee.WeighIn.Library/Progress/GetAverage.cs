using deejayvee.WeighIn.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.Progress
{
    public class GetAverage : GetProgressBase
    {
        public GetAverage(IAwsFactory factory) : base(factory)
        {
        }

        public string Retrieve(string userId, string firstName)
        {
            try
            {
                ProgressAverage average = new ProgressAverage();
                LoadProgress(userId, firstName, average);

                return JsonConvert.SerializeObject(average);
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error getting Average Progress with userId=\"{userId}\" and firstName=\"{firstName}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
