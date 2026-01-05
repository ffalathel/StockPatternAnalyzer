using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for bearish hammer (also called hanging man)
    /// Hammer that closed lower than it opened
    /// </summary>
    public class Recognizer_BearishHammer : Recognizer
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Recognizer_BearishHammer() : base("Bearish Hammer", 1)
        {
        }

        /// <summary>
        /// Checks for bearish hammer at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if bearish hammer found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            return candlesticks[index].isBearishHammer;
        }
    }
}
