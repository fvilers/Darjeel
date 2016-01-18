using System;
using System.Diagnostics;

namespace Darjeel.Infrastructure
{
    internal class Logging
    {
        private static readonly Lazy<TraceSource> DarjeelTraceSource = new Lazy<TraceSource>(() => new TraceSource("Darjeel"));

        public TraceSource Darjeel => DarjeelTraceSource.Value;
    }
}
