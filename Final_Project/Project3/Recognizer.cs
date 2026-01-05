using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// This is my abstract base class for all the pattern recognizers
    /// Each specific recognizer (like Doji, Hammer, etc) will inherit from this
    /// </summary>
    public abstract class Recognizer
    {
        /// <summary>
        /// The name of the pattern this recognizer looks for
        /// Like "Doji" or "Bullish Engulfing"
        /// </summary>
        public string patternName { get; protected set; }

        /// <summary>
        /// How many candlesticks this pattern needs
        /// 1 for single candle patterns, 2 for two-candle patterns like engulfing
        /// </summary>
        public int patternSize { get; protected set; }

        /// <summary>
        /// Constructor that sets up the recognizer with its name and size
        /// Protected so only child classes can use it
        /// </summary>
        /// <param name="patternName">Name of the pattern</param>
        /// <param name="patternSize">Number of candlesticks needed</param>
        protected Recognizer(string patternName, int patternSize)
        {
            // save the pattern name so we can display it later
            this.patternName = patternName;
            // save the size so we know how many candles to look at
            this.patternSize = patternSize;
        }

        /// <summary>
        /// This is the main method that checks if a pattern exists at a specific index
        /// Each child class has to implement this with its own logic
        /// </summary>
        /// <param name="candlesticks">List of candlesticks to check</param>
        /// <param name="index">Which candlestick to check at</param>
        /// <returns>True if the pattern is found, false otherwise</returns>
        public abstract bool Recognize(List<aSmartCandlestick> candlesticks, int index);

        /// <summary>
        /// Helper method to check if an index is valid for this pattern
        /// I need this because 2-candle patterns need to check the previous candle too
        /// </summary>
        /// <param name="candlesticks">List of candlesticks</param>
        /// <param name="index">Index to check</param>
        /// <returns>True if the index is valid for this pattern size</returns>
        protected virtual bool IndexIsValid(List<aSmartCandlestick> candlesticks, int index)
        {
            // can't do anything with null or empty list
            if (candlesticks == null || candlesticks.Count == 0)
            {
                return false;
            }

            // make sure index is in bounds
            if (index < 0 || index >= candlesticks.Count)
            {
                return false;
            }

            // for 2-candle patterns, need index >= 1 so we can look at index-1
            // for 3-candle patterns would need index >= 2, etc
            if (index < patternSize - 1)
            {
                return false;
            }

            return true;
        }
    }
}
