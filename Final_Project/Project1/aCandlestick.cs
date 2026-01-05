using System;
using System.Globalization;

namespace Project1
{
    /// <summary>
    /// This is my advanced candlestick class that can read itself from a CSV line
    /// It has all the OHLC data and volume for one trading day
    /// I made it so it can be created in different ways
    /// </summary>
    public class aCandlestick
    {
        // These are the characters I use to split CSV lines (comma, tab, and handle quoted fields)
        private static char[] delimiters = { ',', '\t' };

        /// <summary>
        /// The date for this trading day
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// The opening price for this day
        /// </summary>
        public decimal open { get; set; }

        /// <summary>
        /// The highest price the stock hit today
        /// </summary>
        public decimal high { get; set; }

        /// <summary>
        /// The lowest price the stock hit today
        /// </summary>
        public decimal low { get; set; }

        /// <summary>
        /// The closing price for this day
        /// </summary>
        public decimal close { get; set; }

        /// <summary>
        /// How many shares were traded today
        /// </summary>
        public ulong volume { get; set; }

        /// <summary>
        /// Default constructor for aCandlestick object
        /// Creates an empty candlestick with default values
        /// </summary>
        public aCandlestick()
        {
            // Default constructor - properties will have default values
        }

        /// <summary>
        /// Parameterized constructor to create a candlestick with all data
        /// </summary>
        /// <param name="date">The date for this candlestick</param>
        /// <param name="open">Opening price</param>
        /// <param name="high">Highest price</param>
        /// <param name="low">Lowest price</param>
        /// <param name="close">Closing price</param>
        /// <param name="volume">Trading volume</param>
        public aCandlestick(DateTime date, decimal open, decimal high, decimal low, decimal close, ulong volume)
        {
            // Set the date for this candlestick
            this.date = date;
            // Set the opening price
            this.open = open;
            // Set the highest price
            this.high = high;
            // Set the lowest price
            this.low = low;
            // Set the closing price
            this.close = close;
            // Set the trading volume
            this.volume = volume;
        }

        /// <summary>
        /// Copy constructor that creates a new aCandlestick from an existing one
        /// </summary>
        /// <param name="otherCandlestick">The candlestick to copy from</param>
        public aCandlestick(aCandlestick otherCandlestick)
        {
            // Copy the date from the other candlestick
            this.date = otherCandlestick.date;
            // Copy the opening price
            this.open = otherCandlestick.open;
            // Copy the highest price
            this.high = otherCandlestick.high;
            // Copy the lowest price
            this.low = otherCandlestick.low;
            // Copy the closing price
            this.close = otherCandlestick.close;
            // Copy the trading volume
            this.volume = otherCandlestick.volume;
        }

        /// <summary>
        /// Constructor that parses a CSV line directly into a candlestick object
        /// Expected format: Ticker,Period,Date,Open,High,Low,Close,Volume,LogReturn,Sector
        /// Example: AAPL,M,3/31/23,158.86,165,155.98,164.90,327195933,0,TechnologyHardwareAndEquipment
        /// </summary>
        /// <param name="line">CSV line containing candlestick data</param>
        public aCandlestick(string line)
        {
            // Use invariant culture to ensure consistent parsing regardless of system locale
            CultureInfo provider = CultureInfo.InvariantCulture;

            // Split the line by tab delimiter (your CSV appears to be tab-separated)
            string[] strings = line.Split('\t');

            // If we don't have enough columns, try comma separator
            if (strings.Length < 8)
            {
                // Split by comma delimiter as fallback
                strings = line.Split(',');
            }

            // Clean up quoted values by removing quotes
            for (int i = 0; i < strings.Length; i++)
            {
                // Remove leading and trailing quotes and whitespace
                strings[i] = strings[i].Trim().Trim('"');
            }

            // Your CSV format: Ticker, Period, Date, Open, High, Low, Close, Volume, LogReturn, Sector
            // Index:              0       1       2     3      4     5      6       7         8         9

            // Get the date string from column index 2 (third column)
            string dateString = strings[2];

            // Try to parse the date - handle multiple formats
            try
            {
                // Try parsing as M/d/yy format (e.g., 3/31/23)
                date = DateTime.Parse(dateString, CultureInfo.InvariantCulture);
            }
            catch
            {
                try
                {
                    // Try parsing as yyyy-MM-dd format (e.g., 2023-03-31)
                    date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                catch
                {
                    // If both fail, try general DateTime.Parse as last resort
                    date = DateTime.Parse(dateString);
                }
            }

            // Parse the numeric values from columns 3-7
            // Column 3: Open price
            open = Convert.ToDecimal(strings[3]);
            // Column 4: High price
            high = Convert.ToDecimal(strings[4]);
            // Column 5: Low price
            low = Convert.ToDecimal(strings[5]);
            // Column 6: Close price
            close = Convert.ToDecimal(strings[6]);
            // Column 7: Volume
            volume = Convert.ToUInt64(strings[7]);
        }
    }
}