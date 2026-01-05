using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for hammer pattern
    /// Small body at top with long lower tail
    /// Usually bullish reversal signal
    /// </summary>
    public class Recognizer_Hammer : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_Hammer() : base("Hammer", 1)
        {
        }

        /// <summary>
        /// Checks for hammer at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if hammer found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isHammer;
        }
    }
}
