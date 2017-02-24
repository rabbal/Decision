using System;

namespace Decision.Common.Logging
{
    public interface ILogger
    {
        void Log(Exception exception);
        void Log(Exception exception,string message);
    }
}
