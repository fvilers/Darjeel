using System;
using System.Diagnostics;

namespace Darjeel.Diagnostics.Extensions
{
    public static class TraceSourceExtensions
    {
        public static void TraceWarning(this TraceSource traceSource, string message)
        {
            if (traceSource == null) throw new ArgumentNullException(nameof(traceSource));
            if (message == null) throw new ArgumentNullException(nameof(message));

            traceSource.TraceWarning(message, null);
        }

        public static void TraceWarning(this TraceSource traceSource, string format, params object[] args)
        {
            if (traceSource == null) throw new ArgumentNullException(nameof(traceSource));
            if (format == null) throw new ArgumentNullException(nameof(format));

            traceSource.TraceEvent(TraceEventType.Warning, 0, format, args);
        }

        public static void TraceError(this TraceSource traceSource, string message)
        {
            if (traceSource == null) throw new ArgumentNullException(nameof(traceSource));
            if (message == null) throw new ArgumentNullException(nameof(message));

            traceSource.TraceError(message, null);
        }

        public static void TraceError(this TraceSource traceSource, string format, params object[] args)
        {
            if (traceSource == null) throw new ArgumentNullException(nameof(traceSource));
            if (format == null) throw new ArgumentNullException(nameof(format));

            traceSource.TraceEvent(TraceEventType.Error, 0, format, args);
        }
    }
}
