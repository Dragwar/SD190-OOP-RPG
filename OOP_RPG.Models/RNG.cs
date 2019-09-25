using System;
using System.Threading;

namespace OOP_RPG.Models
{
    /// <summary>
    /// Holds a <see cref="ThreadStaticAttribute"/> <see cref="System.Random"/> property
    /// </summary>
    public static class RNG
    {
        /// <summary>
        /// underlying random
        /// </summary>
        [ThreadStatic]
        private static readonly Random _random;

        /// <summary>
        /// Get underlying random
        /// </summary>
        /// <see cref="https://stackoverflow.com/questions/38530207/how-to-make-a-c-sharp-thread-safe-random-number-generator"/>
        public static Random Random => _random ?? new Random((int)((1 + Thread.CurrentThread.ManagedThreadId) * DateTime.UtcNow.Ticks));

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        /// that is, the range of return values includes minValue but not maxValue. If minValue
        /// equals maxValue, minValue is returned.
        /// </returns>
        public static int Next() => Random.Next();

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// minValue is greater than maxValue.
        /// </exception>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        /// that is, the range of return values includes minValue but not maxValue. If minValue
        /// equals maxValue, minValue is returned.
        /// </returns>
        public static int Next(int minValue, int maxValue) => Random.Next(minValue, maxValue);

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="maxValue"></param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// maxValue is less than 0.
        /// </exception>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to minValue and less than maxValue;
        /// that is, the range of return values includes minValue but not maxValue. If minValue
        /// equals maxValue, minValue is returned.
        /// </returns>
        public static int Next(int maxValue) => Random.Next(maxValue);
    }
}
