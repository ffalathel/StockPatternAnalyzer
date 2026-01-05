using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bullish hammer
    /// Hammer that closed higher than it opened
    /// </summary>
    public class Recognizer_BullishHammer : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BullishHammer() : base("Bullish Hammer", 1)
        {
        }

        /// <summary>
        /// Checks for bullish hammer at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bullish hammer found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isBullishHammer;
        }
    }
}
