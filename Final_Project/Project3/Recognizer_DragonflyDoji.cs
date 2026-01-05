using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for dragonfly doji pattern
    /// Looks like a T - long lower tail, tiny body at top
    /// Usually a bullish reversal signal
    /// </summary>
    public class Recognizer_DragonflyDoji : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_DragonflyDoji() : base("Dragonfly Doji", 1)
        {
        }

        /// <summary>
        /// Checks for dragonfly doji at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if dragonfly doji found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isDragonflyDoji;
        }
    }
}
