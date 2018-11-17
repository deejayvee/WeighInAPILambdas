using System;
using System.Collections.Generic;
using System.Text;

namespace deejayvee.WeighIn.Library
{
    public abstract class WeighInBase : IDisposable
    {
        protected IAwsFactory Factory { get; set; }

        protected WeighInBase(IAwsFactory factory)
        {
            Factory = factory;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Factory = null;
                }

                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
