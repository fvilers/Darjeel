using System;
using System.Diagnostics;

namespace Darjeel.Infrastructure.Azure
{
    internal static class Logging
    {
        private static readonly Lazy<TraceSource> DarjeelAzureTraceSource = new Lazy<TraceSource>(() => new TraceSource("Darjeel.Azure"));

        public static TraceSource DarjeelAzure => DarjeelAzureTraceSource.Value;
    }
}
