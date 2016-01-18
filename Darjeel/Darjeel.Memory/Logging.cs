using System;
using System.Diagnostics;

namespace Darjeel.Memory
{
    internal static class Logging
    {
        private static readonly Lazy<TraceSource> DarjeelMemoryTraceSource = new Lazy<TraceSource>(() => new TraceSource("Darjeel.Memory"));

        public static TraceSource DarjeelMemory => DarjeelMemoryTraceSource.Value;
    }
}
