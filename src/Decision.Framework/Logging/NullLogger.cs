using System;

namespace Decision.Framework.Logging
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
