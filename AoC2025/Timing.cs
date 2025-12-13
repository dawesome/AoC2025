using System;
using System.Diagnostics;

namespace AoC2025
{
    public static class Timing
    {
        // Measures an action and returns elapsed time.
        public static TimeSpan Time(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            var sw = Stopwatch.StartNew();
            try
            {
                action();
            }
            finally
            {
                sw.Stop();
            }
            return sw.Elapsed;
        }

        // Measures a function, returns its result and elapsed time.
        public static (T result, TimeSpan elapsed) Time<T>(Func<T> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));
            var sw = Stopwatch.StartNew();
            try
            {
                T result = func();
                sw.Stop();
                return (result, sw.Elapsed);
            }
            catch
            {
                sw.Stop();
                throw;
            }
        }
    }
}