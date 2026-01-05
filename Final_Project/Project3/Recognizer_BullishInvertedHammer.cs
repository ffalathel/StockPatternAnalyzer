using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bullish inverted hammer
    /// Inverted hammer that closed higher
    /// </summary>
    public class Recognizer_BullishInvertedHammer : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BullishInvertedHammer() : base("Bullish Inverted Hammer", 1)
        {
        }

        /// <summary>
        /// Checks for bullish inverted hammer at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bullish inverted hammer found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isBullishInvertedHammer;
        }
    }
}
