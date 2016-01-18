using System;
using System.Diagnostics;

namespace Darjeel.Infrastructure.EntityFramework
{
    internal static class Logging
    {
        private static readonly Lazy<TraceSource> DarjeelEntityFrameworkTraceSource = new Lazy<TraceSource>(() => new TraceSource("Darjeel.EntityFramework"));

        public static TraceSource DarjeelEntityFramework => DarjeelEntityFrameworkTraceSource.Value;
    }
}
