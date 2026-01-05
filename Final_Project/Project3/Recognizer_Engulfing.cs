using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for engulfing pattern (either bullish or bearish)
    /// The second candle's body completely covers the first candle's body
    /// </summary>
    public class Recognizer_Engulfing : Recognizer
    {
        /// <summary>
        /// Constructor - engulfing needs 2 candles
        /// </summary>
        public Recognizer_Engulfing() : base("Engulfing", 2)
        {
        }

        /// <summary>
        /// Checks for engulfing pattern at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if engulfing found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            // need at least 2 candles
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            // get the two candles
            aSmartCandlestick previous = candlesticks[index - 1];
            aSmartCandlestick current = candlesticks[index];

            // check for bullish engulfing
            bool isBullishEngulfing = previous.isBearish &&
                                      current.isBullish &&
                                      current.bottomPrice < previous.bottomPrice &&
                                      current.topPrice > previous.topPrice;

            // check for bearish engulfing
            bool isBearishEngulfing = previous.isBullish &&
                                      current.isBearish &&
                                      current.topPrice > previous.topPrice &&
                                      current.bottomPrice < previous.bottomPrice;

            return isBullishEngulfing || isBearishEngulfing;
        }
    }
}
