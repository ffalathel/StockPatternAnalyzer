using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bearish doji pattern
    /// A doji that closed slightly lower than it opened
    /// </summary>
    public class Recognizer_BearishDoji : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BearishDoji() : base("Bearish Doji", 1)
        {
        }

        /// <summary>
        /// Checks for bearish doji at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bearish doji found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            // needs to be both a doji AND bearish
            return candlesticks[index].isDoji && candlesticks[index].isBearish;
        }
    }
}
