using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bearish harami pattern
    /// First candle is bullish, second is bearish and inside the first
    /// Potential bearish reversal
    /// </summary>
    public class Recognizer_BearishHarami : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BearishHarami() : base("Bearish Harami", 2)
        {
        }

        /// <summary>
        /// Checks for bearish harami at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bearish harami found, false otherwise</returns>
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
            // second candle's body must be inside the first
            bool bodyInside = current.topPrice < previous.topPrice &&
                             current.bottomPrice > previous.bottomPrice;

            return previousIsBullish && currentIsBearish && bodyInside;
        }
    }
}
