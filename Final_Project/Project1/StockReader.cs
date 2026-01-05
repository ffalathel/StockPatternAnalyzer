using System;
using System.Collections.Generic;
using System.IO;

namespace Project1
{
    /// <summary>
    /// This class helps me read stock data from CSV files
    /// I use this to parse CSV files that have stock market data in them
    /// </summary>
    public class StockReader
    {
        /// <summary>
        /// This method reads candlestick data from a CSV file and gives me back a list of candlesticks
        /// It goes through the file line by line and skips the header row
        /// </summary>
        /// <param name="filename">The full path to the CSV file I want to read</param>
        /// <returns>List of candlestick objects from the file, or null if something goes wrong</returns>
        public List<aCandlestick> ReadCandlesticksFromCsv(string filename)
        {
            // I'll start with null and fill this up as I read the file
            List<aCandlestick> candlesticks = null;

            // I need to wrap this in try-catch in case the file doesn't exist or something
            try
            {
                // This will hold all the text from the file
                string text;

                // I'm using a using statement so the file gets closed automatically
                using (StreamReader reader = new StreamReader(filename))
                {
                    // Read everything from the file into one big string
                    text = reader.ReadToEnd();
                }

                // I need to split the text by newlines to get individual lines
                char[] lineDelimiter = new char[] { '\n' };
                // Split by newlines and get rid of any empty lines
                string[] lines = text.Split(lineDelimiter, StringSplitOptions.RemoveEmptyEntries);

                // Create a list with enough space for all the lines (minus the header)
                candlesticks = new List<aCandlestick>(lines.Length);

                // Start from 1 to skip the header row with column names
                for (int i = 1; i < lines.Length; i++)
                {
                    // Get the current line I'm working on
                    string line = lines[i];

                    // Create a candlestick object from this line using the string constructor
                    aCandlestick c = new aCandlestick(line);

                    // Add this candlestick to my list
                    candlesticks.Add(c);
                }
            }
            catch (IOException e)
            {
                // If something goes wrong, print an error message
                Console.WriteLine("The file could not be read:");
                // Print the specific error details
                Console.WriteLine(e.Message);
            }

            // Return my list of candlesticks (might be null if something went wrong)
            return candlesticks;
        }
    }
}