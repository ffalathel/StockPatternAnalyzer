using System;
using Project1; // I'm reusing the aCandlestick from Project1

namespace Project3
{
    /// <summary>
    /// This is my smart candlestick class that adds pattern recognition to the basic candlestick
    /// I inherit from aCandlestick so I get all the OHLC data and then add extra properties
    /// for detecting patterns like doji, hammer, marubozu, etc
    /// </summary>
    public class aSmartCandlestick : aCandlestick
    {
        /// <summary>
        /// The total price range for this candlestick (high minus low)
        /// I use this to see how much the price moved during the period
        /// </summary>
        public decimal range
        {
            get
            {
                // just subtract low from high to get the total range
                return high - low;
            }
        }

        /// <summary>
        /// The size of the body (difference between open and close)
        /// I use Math.Abs because it doesn't matter which one is bigger
        /// </summary>
        public decimal bodyRange
        {
            get
            {
                // absolute value since close could be above or below open
                return Math.Abs(close - open);
            }
        }

        /// <summary>
        /// How long the upper tail/wick is
        /// This is the distance from the top of the body to the high
        /// </summary>
        public decimal upperTailRange
        {
            get
            {
                // upper tail goes from top of body up to the high
                return high - topPrice;
            }
        }

        /// <summary>
        /// How long the lower tail/wick is
        /// This is the distance from the low up to the bottom of the body
        /// </summary>
        public decimal lowerTailRange
        {
            get
            {
                // lower tail goes from low up to bottom of body
                return bottomPrice - low;
            }
        }

        /// <summary>
        /// The top of the body - whichever is higher, open or close
        /// </summary>
        public decimal topPrice
        {
            get
            {
                // top of body is the bigger of open and close
                return Math.Max(open, close);
            }
        }

        /// <summary>
        /// The bottom of the body - whichever is lower, open or close
        /// </summary>
        public decimal bottomPrice
        {
            get
            {
                // bottom of body is the smaller of open and close
                return Math.Min(open, close);
            }
        }

        /// <summary>
        /// True if this is a bullish candlestick (price went up)
        /// </summary>
        public bool isBullish
        {
            get
            {
                // bullish means we closed higher than we opened
                return close > open;
            }
        }

        /// <summary>
        /// True if this is a bearish candlestick (price went down)
        /// </summary>
        public bool isBearish
        {
            get
            {
                // bearish means we closed lower than we opened
                return close < open;
            }
        }

        /// <summary>
        /// True if price didn't really change (open equals close)
        /// </summary>
        public bool isNeutral
        {
            get
            {
                // neutral when open and close are the same
                return close == open;
            }
        }

        /// <summary>
        /// Checks if this candlestick is a doji pattern
        /// A doji has a tiny body compared to its range - signals indecision
        /// </summary>
        public bool isDoji
        {
            get
            {
                // if there's no range at all, it's basically a perfect doji
                if (range == 0)
                {
                    return true;
                }
                // doji if the body is less than 10% of the total range
                return bodyRange < 0.10m * range;
            }
        }

        /// <summary>
        /// A dragonfly doji has a long lower tail and tiny upper tail
        /// Looks like a T shape, usually bullish
        /// </summary>
        public bool isDragonflyDoji
        {
            get
            {
                // can't be dragonfly if there's no price movement
                if (range == 0)
                {
                    return false;
                }
                // needs to be a doji with tiny upper tail and longer lower tail
                return isDoji && 
                       upperTailRange < 0.1m * range && 
                       lowerTailRange > 0.1m * range;
            }
        }

        /// <summary>
        /// A gravestone doji has a long upper tail and tiny lower tail
        /// Looks like an upside down T, usually bearish
        /// </summary>
        public bool isGravestoneDoji
        {
            get
            {
                // can't be gravestone if there's no price movement
                if (range == 0)
                {
                    return false;
                }
                // needs to be a doji with tiny lower tail and longer upper tail
                return isDoji && 
                       lowerTailRange < 0.1m * range && 
                       upperTailRange > 0.1m * range;
            }
        }

        /// <summary>
        /// A marubozu has almost no tails - the body takes up the whole range
        /// Shows strong conviction in one direction
        /// </summary>
        public bool isMarubozu
        {
            get
            {
                // can't be marubozu without price movement
                if (range == 0)
                {
                    return false;
                }
                // marubozu if body is more than 90% of range
                return bodyRange > 0.90m * range;
            }
        }

        /// <summary>
        /// A bullish marubozu - strong buying pressure, price went up with no tails
        /// </summary>
        public bool isBullishMarubozu
        {
            get
            {
                // needs to be a marubozu AND bullish
                return isMarubozu && isBullish;
            }
        }

        /// <summary>
        /// A bearish marubozu - strong selling pressure, price went down with no tails
        /// </summary>
        public bool isBearishMarubozu
        {
            get
            {
                // needs to be a marubozu AND bearish
                return isMarubozu && isBearish;
            }
        }

        /// <summary>
        /// A hammer has a small body at the top with a long lower tail
        /// Usually a bullish reversal signal
        /// </summary>
        public bool isHammer
        {
            get
            {
                // can't be hammer without price movement
                if (range == 0)
                {
                    return false;
                }
                // need some body to compare tails against
                if (bodyRange == 0)
                {
                    return false;
                }
                // hammer needs long lower tail, small upper tail, small body
                return lowerTailRange >= 1.5m * bodyRange && 
                       upperTailRange < bodyRange && 
                       bodyRange < 0.5m * range;
            }
        }

        /// <summary>
        /// Bullish hammer - a hammer pattern that closed higher than it opened
        /// </summary>
        public bool isBullishHammer
        {
            get
            {
                // hammer that's also bullish
                return isHammer && isBullish;
            }
        }

        /// <summary>
        /// Bearish hammer - a hammer pattern that closed lower (also called hanging man)
        /// </summary>
        public bool isBearishHammer
        {
            get
            {
                // hammer that's also bearish
                return isHammer && isBearish;
            }
        }

        /// <summary>
        /// An inverted hammer has a small body at the bottom with a long upper tail
        /// </summary>
        public bool isInvertedHammer
        {
            get
            {
                // can't be inverted hammer without price movement
                if (range == 0)
                {
                    return false;
                }
                // need some body to compare tails against
                if (bodyRange == 0)
                {
                    return false;
                }
                // inverted hammer needs long upper tail, small lower tail, small body
                return upperTailRange >= 1.5m * bodyRange && 
                       lowerTailRange < bodyRange && 
                       bodyRange < 0.5m * range;
            }
        }

        /// <summary>
        /// Bullish inverted hammer - inverted hammer that closed higher
        /// </summary>
        public bool isBullishInvertedHammer
        {
            get
            {
                // inverted hammer that's also bullish
                return isInvertedHammer && isBullish;
            }
        }

        /// <summary>
        /// Bearish inverted hammer - inverted hammer that closed lower (shooting star)
        /// </summary>
        public bool isBearishInvertedHammer
        {
            get
            {
                // inverted hammer that's also bearish
                return isInvertedHammer && isBearish;
            }
        }

        /// <summary>
        /// Default constructor - just calls the base constructor
        /// </summary>
        public aSmartCandlestick() : base()
        {
            // nothing extra to do here, base class handles it
        }

        /// <summary>
        /// Constructor that takes all the OHLCV data
        /// </summary>
        /// <param name="date">Date of this candlestick</param>
        /// <param name="open">Opening price</param>
        /// <param name="high">High price</param>
        /// <param name="low">Low price</param>
        /// <param name="close">Closing price</param>
        /// <param name="volume">Volume traded</param>
        public aSmartCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
            : base(date, open, high, low, close, volume)
        {
            // base constructor sets all the values, pattern properties are computed automatically
        }

        /// <summary>
        /// Constructor that copies from an existing aCandlestick
        /// I use this to convert regular candlesticks to smart ones
        /// </summary>
        /// <param name="candlestick">The candlestick to copy from</param>
        public aSmartCandlestick(aCandlestick candlestick) : base()
        {
            // copy all the values over from the source candlestick
            this.date = candlestick.date;
            this.open = candlestick.open;
            this.high = candlestick.high;
            this.low = candlestick.low;
            this.close = candlestick.close;
            this.volume = candlestick.volume;
            // pattern properties get computed automatically from these values
        }

        /// <summary>
        /// Constructor that parses a CSV line
        /// Just passes the line to the base class which knows how to parse it
        /// </summary>
        /// <param name="line">CSV line with candlestick data</param>
        public aSmartCandlestick(string line) : base(line)
        {
            // base class handles the CSV parsing
        }
    }
}
