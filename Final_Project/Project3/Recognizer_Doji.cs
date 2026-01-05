using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Recognizer for the Doji pattern
    /// A doji has a tiny body - signals indecision in the market
    /// </summary>
    public class Recognizer_Doji : Recognizer
    {
        /// <summary>
        /// Constructor - sets up the pattern name and size
        /// </summary>
        public Recognizer_Doji() : base("Doji", 1)
        {
            // doji is a single candlestick pattern
        }

        /// <summary>
        /// Checks if there's a doji at the given index
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>True if doji found, false otherwise</returns>
        public override bool Recognize(List<aSmartCandlestick> candlesticks, int index)
        {
            // make sure index is valid
            if (!IndexIsValid(candlesticks, index))
            {
                return false;
            }

            // check the isDoji property
            return candlesticks[index].isDoji;
        }
    }
}
