using System;
using System.Diagnostics;
using System.Web;
using Elmah;

namespace Decision.Framework.Logging
{
    /// <summary>
    /// Log <see cref="Exception"/> objects.
    /// </summary>
    public sealed class Logger : ILogger
    {
        public void Log(Exception exception)
        {

            // Log to Tracing.
            Trace.TraceError(exception.ToString());
            // Log to Elmah.
            ErrorSignal.FromCurrentContext().Raise(exception, HttpContext.Current);
        }
        public void Log(Exception exception, string message)
        {
            // Log to Tracing.
            Trace.TraceError(new Exception(message, exception).ToString());
            // Log to Elmah.
            ErrorSignal.FromCurrentContext().Raise(new Exception(message, exception), HttpContext.Current);
        }
    }
}