//-----------------------------------------------------------------------
// <copyright file="PerfTimer.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace MinoriEditorShell.VirtualCanvas.Platforms.Wpf.Models
{
    /// <summary>
    /// This class implements a high precision timer using the Win32 QueryPerformanceCounter API.
    /// Typical usage:
    /// <code>
    ///     PerfTimer t = new PerfTimer();
    ///     t.Start();
    ///     ...
    ///     t.Stop();
    ///     Int64 ms = t.GetDuration();
    /// </code>
    /// You can also use it to add up a bunch of times in a loop and report average, mininum
    /// and maximum times.
    /// </summary>
    internal class MesPerfTimer
    {
        private long _start;
        private long _end;
        private readonly long _freq;
        private long _min;
        private long _max;
        private long _count;
        private long _sum;

        /// <summary>
        ///
        /// </summary>
        public MesPerfTimer()
        {
            _start = _end = 0;
            QueryPerformanceFrequency(ref _freq);
            _min = _max = _count = _sum = 0;
        }

        /// <summary>
        /// Set current time as the start time.
        /// </summary>
        public void Start()
        {
            _start = GetCurrentTime();
            _end = _start;
        }

        /// <summary>
        /// Set the current time as the end time.
        /// </summary>
        public void Stop() => _end = GetCurrentTime();

        /// <summary>
        /// Get the time in milliseconds between Start() and Stop().
        /// </summary>
        /// <returns>Milliseconds</returns>
        public long GetDuration() => GetMilliseconds(GetDurationInTicks());

        /// <summary>
        /// Convert the given argument from "ticks" to milliseconds.
        /// </summary>
        /// <param name="ticks">Number of ticks returned from GetTicks()</param>
        /// <returns>Milliseconds</returns>
        public long GetMilliseconds(long ticks) => (ticks * (long)1000) / _freq;

        /// <summary>
        /// Get the time between Start() and Stop() in the highest fidelity possible
        /// as defined by Windows QueryPerformanceFrequency.  Usually this is nanoseconds.
        /// </summary>
        /// <returns>High fidelity tick count</returns>
        public long GetDurationInTicks() => (_end - _start);

        /// <summary>
        /// Get current time in ighest fidelity possible as defined by Windows QueryPerformanceCounter.
        /// Usually this is nanoseconds.
        /// </summary>
        /// <returns>High fidelity tick count</returns>
        public static long GetCurrentTime()
        { // in nanoseconds.
            long i = 0;
            QueryPerformanceCounter(ref i);
            return i;
        }

        // These methods allow you to count up multiple iterations and
        // then get the median, average and percent variation.

        /// <summary>
        /// Add the given time to a running total so we can compute minimum, maximum and average.
        /// </summary>
        /// <param name="time">The time to record</param>
        public void Count(long time)
        {
            if (_min == 0) { _min = time; }
            if (time < _min) { _min = time; }
            if (time > _max) { _max = time; }

            _sum += time;
            _count++;
        }

        /// <summary>
        /// Return the minimum time recorded by the Count() method since the last Clear
        /// </summary>
        /// <returns>The minimum value</returns>
        public long Minimum() => _min;

        /// <summary>
        /// Return the maximum time recorded by the Count() method since the last Clear
        /// </summary>
        /// <returns>The maximum value</returns>
        public long Max() => _max;

        /// <summary>
        /// Return the median of the values recorded by the Count() method since the last Clear
        /// </summary>
        /// <returns>The median value</returns>
        public double Median() => (_min + ((_max - _min) / 2.0));

        /// <summary>
        /// Return the variance in the numbers recorded by the Count() method since the last Clear
        /// </summary>
        /// <returns>Percentage between 0 and 100</returns>
        public double PercentError()
        {
            double spread = (_max - _min) / 2.0;
            double percent = ((double)(spread * 100.0) / _min);
            return percent;
        }

        /// <summary>
        /// Return the avergae of the values recorded by the Count() method since the last Clear
        /// </summary>
        /// <returns>The average value</returns>
        public long Average()
        {
            if (_count == 0) { return 0; }
            return _sum / _count;
        }

        /// <summary>
        /// Reset the timer to its initial state.
        /// </summary>
        public void Clear() => _start = _end = _min = _max = _sum = _count = 0;

        [DllImport("KERNEL32.DLL", EntryPoint = "QueryPerformanceCounter", SetLastError = true,
                    CharSet = CharSet.Unicode, ExactSpelling = true,
                    CallingConvention = CallingConvention.StdCall)]
        private static extern int QueryPerformanceCounter(ref long time);

        [DllImport("KERNEL32.DLL", EntryPoint = "QueryPerformanceFrequency", SetLastError = true,
             CharSet = CharSet.Unicode, ExactSpelling = true,
             CallingConvention = CallingConvention.StdCall)]
        private static extern int QueryPerformanceFrequency(ref long freq);
    }
}