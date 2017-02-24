using System;

namespace Decision.Common.Logging
{
    public class NullLogger : ILogger
    {
        public static NullLogger Instance => new NullLogger();

        public void Log(Exception exception)
        {
        }

        public void Log(Exception exception, string message)
        {
        }
    }
}
