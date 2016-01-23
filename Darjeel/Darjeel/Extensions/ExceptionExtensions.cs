using Newtonsoft.Json;
using System;

namespace Darjeel.Extensions
{
    public static class ExceptionExtensions
    {
        public static string AsJson(this Exception exception, Formatting formatting = Formatting.Indented)
        {
            return exception != null ? JsonConvert.SerializeObject(@exception, formatting) : null;
        }
    }
}
