using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bearish marubozu
    /// Strong bearish candle with no tails - sellers in control
    /// </summary>
    public class Recognizer_BearishMarubozu : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BearishMarubozu() : base("Bearish Marubozu", 1)
        {
        }

        /// <summary>
        /// Checks for bearish marubozu at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bearish marubozu found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isBearishMarubozu;
        }
    }
}
