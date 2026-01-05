using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bullish marubozu
    /// Strong bullish candle with no tails - buyers in control
    /// </summary>
    public class Recognizer_BullishMarubozu : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BullishMarubozu() : base("Bullish Marubozu", 1)
        {
        }

        /// <summary>
        /// Checks for bullish marubozu at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bullish marubozu found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isBullishMarubozu;
        }
    }
}
