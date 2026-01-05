using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bullish harami pattern
    /// First candle is bearish, second is bullish and inside the first
    /// Potential bullish reversal
    /// </summary>
    public class Recognizer_BullishHarami : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BullishHarami() : base("Bullish Harami", 2)
        {
        }

        /// <summary>
        /// Checks for bullish harami at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bullish harami found, false otherwise</returns>
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
            // second candle's body must be inside the first
            bool bodyInside = current.topPrice < previous.topPrice &&
                             current.bottomPrice > previous.bottomPrice;

            return previousIsBearish && currentIsBullish && bodyInside;
        }
    }
}
