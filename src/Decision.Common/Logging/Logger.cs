using System;
using System.Diagnostics;
using System.Web;
using Elmah;
using NTierMvcFramework.Common.Exceptions;

namespace NTierMvcFramework.Common.Logging
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
            Trace.TraceError(new LogException(message, exception).ToString());
            // Log to Elmah.
            ErrorSignal.FromCurrentContext().Raise(new LogException(message, exception), HttpContext.Current);
        }
    }
}