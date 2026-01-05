using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bullish doji pattern
    /// A doji that closed slightly higher than it opened
    /// </summary>
    public class Recognizer_BullishDoji : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BullishDoji() : base("Bullish Doji", 1)
        {
        }

        /// <summary>
        /// Checks for bullish doji at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bullish doji found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            // needs to be both a doji AND bullish
            return candlesticks[index].isDoji && candlesticks[index].isBullish;
        }
    }
}
