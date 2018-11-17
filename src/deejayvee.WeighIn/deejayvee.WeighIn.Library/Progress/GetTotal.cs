using deejayvee.WeighIn.Library.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library.Progress
{
    public class GetTotal : GetProgressBase
    {
        public GetTotal(IAwsFactory factory) : base(factory)
        {
        }

        public string Retrieve(string userId, string firstName)
        {
            try
            {
                ProgressTotal total = new ProgressTotal();
                LoadProgress(userId, firstName, total);

                return JsonConvert.SerializeObject(total);
            }
            catch (Exception ex)
            {
                Factory.Logger.Log($"Error getting Total Progress with userId=\"{userId}\" and firstName=\"{firstName}\"");
                Factory.Logger.Log(ex.Message);
                Factory.Logger.Log(ex.StackTrace);
                throw new WeighInException(ex.Message);
            }
        }
    }
}
