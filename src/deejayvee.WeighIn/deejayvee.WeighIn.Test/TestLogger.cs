using Amazon.Lambda.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Test
{
    public class TestLogger : ILambdaLogger
    {
        protected readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Log(string message)
        {
            log.Info(message);
        }

        public void LogLine(string message)
        {
            Log(message);
        }
    }
}
