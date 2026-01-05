using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for harami pattern (either bullish or bearish)
    /// The second candle's body is completely inside the first candle's body
    /// Harami means "pregnant" in Japanese
    /// </summary>
    public class Recognizer_Harami : Recognizer
    {
        /// <summary>
        /// Constructor - harami needs 2 candles
        /// </summary>
        public Recognizer_Harami() : base("Harami", 2)
        {
        }

        /// <summary>
        /// Checks for harami pattern at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if harami found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            aSmartCandlestick previous = candlesticks[index - 1];
            aSmartCandlestick current = candlesticks[index];

            // check for bullish harami
            bool isBullishHarami = previous.isBearish &&
                                   current.isBullish &&
                                   current.topPrice < previous.topPrice &&
                                   current.bottomPrice > previous.bottomPrice;

            // check for bearish harami
            bool isBearishHarami = previous.isBullish &&
                                   current.isBearish &&
                                   current.topPrice < previous.topPrice &&
                                   current.bottomPrice > previous.bottomPrice;

            return isBullishHarami || isBearishHarami;
        }
    }
}
