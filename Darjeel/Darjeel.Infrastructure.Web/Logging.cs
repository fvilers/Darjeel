using System;
using System.Diagnostics;

namespace Darjeel.Infrastructure.Web
{
    internal static class Logging
    {
        private static readonly Lazy<TraceSource> DarjeelWebTraceSource = new Lazy<TraceSource>(() => new TraceSource("Darjeel.Web"));

        public static TraceSource DarjeelWeb => DarjeelWebTraceSource.Value;
    }
}
