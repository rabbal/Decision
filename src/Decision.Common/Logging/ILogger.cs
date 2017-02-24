using System;

namespace NTierMvcFramework.Common.Logging
{
    /// <summary>
    /// Log <see cref="Exception"/> objects.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        void Log(Exception exception);
        void Log(Exception exception,string message);
    }
}
