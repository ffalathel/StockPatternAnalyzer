using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for gravestone doji pattern
    /// Looks like an upside down T - long upper tail, tiny body at bottom
    /// Usually a bearish reversal signal
    /// </summary>
    public class Recognizer_GravestoneDoji : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_GravestoneDoji() : base("Gravestone Doji", 1)
        {
        }

        /// <summary>
        /// Checks for gravestone doji at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if gravestone doji found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isGravestoneDoji;
        }
    }
}
