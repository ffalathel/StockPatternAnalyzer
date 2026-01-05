using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bearish inverted hammer (shooting star)
    /// Inverted hammer that closed lower
    /// </summary>
    public class Recognizer_BearishInvertedHammer : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BearishInvertedHammer() : base("Bearish Inverted Hammer", 1)
        {
        }

        /// <summary>
        /// Checks for bearish inverted hammer at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bearish inverted hammer found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isBearishInvertedHammer;
        }
    }
}
