﻿using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace Darjeel.Infrastructure.Serialization.Extensions
{
    public static class JsonSerializerSettingsExtensions
    {
        [Conditional("DEBUG")]
        public static void EnsureFormatting(this JsonSerializerSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            Trace.TraceInformation("Configuring JSON writer with indented formatting.");
            settings.Formatting = Formatting.Indented;
        }
    }
}
