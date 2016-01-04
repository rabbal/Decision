using System;
using System.Runtime.InteropServices;

namespace ScannerService.WiaApi
{
    [Serializable]
    public class WiaOperationException : Exception
    {
        private WiaScannerError _errorCode;

        public WiaOperationException(WiaScannerError errorCode)
            : base()
        {
            ErrorCode = errorCode;
        }

        public WiaOperationException(string message, WiaScannerError errorCode)
            : base(message)
        {
            ErrorCode = errorCode;
        }

        public WiaOperationException(string message, Exception innerException)
            : base(message, innerException)
        {
            var comException = innerException as COMException;

            if (comException != null)
                ErrorCode = (WiaScannerError)comException.ErrorCode;
        }

        public WiaOperationException(string message, Exception innerException, WiaScannerError errorCode)
            : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        public WiaOperationException(System.Runtime.Serialization.SerializationInfo info,
             System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
            info.AddValue("ErrorCode", (uint)_errorCode);
        }

        public WiaScannerError ErrorCode
        {
            get { return _errorCode; }
            protected set { _errorCode = value; }
        }

        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info,
             System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
            ErrorCode = (WiaScannerError)info.GetUInt32("ErrorCode");
        }
    }
}
