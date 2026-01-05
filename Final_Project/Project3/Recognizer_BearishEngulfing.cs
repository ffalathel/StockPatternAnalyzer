using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bearish engulfing pattern
    /// First candle is bullish, second is bearish and engulfs the first
    /// Strong bearish reversal signal
    /// </summary>
    public class Recognizer_BearishEngulfing : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BearishEngulfing() : base("Bearish Engulfing", 2)
        {
        }

        /// <summary>
        /// Checks for bearish engulfing at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bearish engulfing found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            aSmartCandlestick previous = candlesticks[index - 1];
            aSmartCandlestick current = candlesticks[index];

            // first candle must be bullish
            bool previousIsBullish = previous.isBullish;
            // second candle must be bearish
            bool currentIsBearish = current.isBearish;
            // second candle's body must engulf the first
            bool bodyEngulfs = current.topPrice > previous.topPrice &&
                              current.bottomPrice < previous.bottomPrice;

            return previousIsBullish && currentIsBearish && bodyEngulfs;
        }
    }
}
