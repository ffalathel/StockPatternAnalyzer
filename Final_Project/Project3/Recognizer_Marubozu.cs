using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for marubozu pattern
    /// A candle with almost no tails - body takes up the whole range
    /// Shows strong conviction
    /// </summary>
    public class Recognizer_Marubozu : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_Marubozu() : base("Marubozu", 1)
        {
        }

        /// <summary>
        /// Checks for marubozu at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if marubozu found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isMarubozu;
        }
    }
}
