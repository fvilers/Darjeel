using System;
using System.Diagnostics;

namespace Darjeel
{
    internal static class Logging
    {
        private static readonly Lazy<TraceSource> DarjeelTraceSource = new Lazy<TraceSource>(() => new TraceSource("Darjeel"));

        public static TraceSource Darjeel => DarjeelTraceSource.Value;
    }
}
