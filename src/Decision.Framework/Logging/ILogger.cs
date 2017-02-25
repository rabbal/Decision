using System;

namespace Decision.Framework.Logging
{
    public interface ILogger
    {
        void Log(Exception exception);
        void Log(Exception exception,string message);
    }
}
