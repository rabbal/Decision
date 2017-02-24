using System;

namespace Decision.Common.Misc
{
    public abstract class DisposableClass : IDisposable
    {
        private bool _disposed;

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ~DisposableClass()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
            }

            // release any unmanaged objects
            // set the object references to null

            _disposed = true;
        }
    }


//    public class SubDisposableClass :
//    DisposableClass
//    {
//        private bool _disposed;

//        // a finalizer is not necessary, as it is inherited from
//        // the base class

//        protected override void Dispose(bool disposing)
//        {
//            if (!_disposed)
//            {
//                if (disposing)
//                {
//                    // free other managed objects that implement
//                    // IDisposable only
//                }

//                // release any unmanaged objects
//                // set object references to null

//                _disposed = true;
//            }

//            base.Dispose(disposing);
//        }
//    }
}