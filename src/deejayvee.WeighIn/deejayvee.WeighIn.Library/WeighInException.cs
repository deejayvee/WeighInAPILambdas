using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace deejayvee.WeighIn.Library
{
    [Serializable]
    public class WeighInException : Exception
    {
        public WeighInException(string message) : base(message)
        {
        }

        public WeighInException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WeighInException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
