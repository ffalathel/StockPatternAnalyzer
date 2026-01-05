using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bullish engulfing pattern
    /// First candle is bearish, second is bullish and engulfs the first
    /// Strong bullish reversal signal
    /// </summary>
    public class Recognizer_BullishEngulfing : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BullishEngulfing() : base("Bullish Engulfing", 2)
        {
        }

        /// <summary>
        /// Checks for bullish engulfing at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bullish engulfing found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            aSmartCandlestick previous = candlesticks[index - 1];
            aSmartCandlestick current = candlesticks[index];

            // first candle must be bearish
            bool previousIsBearish = previous.isBearish;
            // second candle must be bullish
            bool currentIsBullish = current.isBullish;
            // second candle's body must engulf the first
            bool bodyEngulfs = current.bottomPrice < previous.bottomPrice &&
                              current.topPrice > previous.topPrice;

            return previousIsBearish && currentIsBullish && bodyEngulfs;
        }
    }
}
