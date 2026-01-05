using System;

namespace Project1
{
    /// <summary>
    /// This is a simple candlestick class that holds OHLC data and volume for one trading day
    /// I use this to represent one day's worth of stock market data
    /// </summary>
    public class Candlestick
    {
        /// <summary>
        /// The date for this trading day
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The opening price for this day
        /// </summary>
        public decimal Open { get; set; }

        /// <summary>
        /// The highest price the stock hit today
        /// </summary>
        public decimal High { get; set; }

        /// <summary>
        /// The lowest price the stock hit today
        /// </summary>
        public decimal Low { get; set; }

        /// <summary>
        /// The closing price for this day
        /// </summary>
        public decimal Close { get; set; }

        /// <summary>
        /// How many shares were traded today
        /// </summary>
        public ulong Volume { get; set; }

        /// <summary>
        /// Default constructor - creates an empty candlestick
        /// </summary>
        public Candlestick()
        {
            // Default constructor - properties will have default values
        }

        /// <summary>
        /// Constructor that lets me create a candlestick with all the data at once
        /// </summary>
        /// <param name="date">The date for this candlestick</param>
        /// <param name="open">Opening price</param>
        /// <param name="high">Highest price</param>
        /// <param name="low">Lowest price</param>
        /// <param name="close">Closing price</param>
        /// <param name="volume">Trading volume</param>
        public Candlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
        {
            // Store the date for this candlestick
            this.Date = date;
            // Store the opening price
            this.Open = open;
            // Store the highest price
            this.High = high;
            // Store the lowest price
            this.Low = low;
            // Store the closing price
            this.Close = close;
            // Store the trading volume
            this.Volume = volume;
        }
    }
}