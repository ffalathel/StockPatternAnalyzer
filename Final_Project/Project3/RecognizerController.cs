using System;
using System.Collections.Generic;
using System.Linq;

namespace Project3
{
    /// <summary>
    /// This is my controller class that manages all the pattern recognizers
    /// I use this to run pattern recognition and get results
    /// </summary>
    public class RecognizerController
    {
        // list to hold all my recognizer objects
        private List<Recognizer> recognizers;

        /// <summary>
        /// Gets a list of all the pattern names I can recognize
        /// Good for populating a combobox
        /// </summary>
        public List<string> patternNames
        {
            get
            {
                // pull out just the names from all recognizers
                return recognizers.Select(r => r.patternName).ToList();
            }
        }

        /// <summary>
        /// How many recognizers I have
        /// </summary>
        public int RecognizerCount
        {
            get { return recognizers.Count; }
        }

        /// <summary>
        /// Constructor - sets up all the recognizers
        /// </summary>
        public RecognizerController()
        {
            // create my list
            recognizers = new List<Recognizer>();
            // add all the recognizers to it
            InitializeRecognizers();
        }

        /// <summary>
        /// This is where I create all the recognizer objects and add them to my list
        /// </summary>
        private void InitializeRecognizers()
        {
            // doji patterns - these signal indecision
            recognizers.Add(new Recognizer_Doji());
            recognizers.Add(new Recognizer_BullishDoji());
            recognizers.Add(new Recognizer_BearishDoji());
            recognizers.Add(new Recognizer_DragonflyDoji());
            recognizers.Add(new Recognizer_GravestoneDoji());

            // marubozu patterns - strong conviction, no tails
            recognizers.Add(new Recognizer_Marubozu());
            recognizers.Add(new Recognizer_BullishMarubozu());
            recognizers.Add(new Recognizer_BearishMarubozu());

            // hammer patterns - potential reversal signals
            recognizers.Add(new Recognizer_Hammer());
            recognizers.Add(new Recognizer_BullishHammer());
            recognizers.Add(new Recognizer_BearishHammer());

            // inverted hammer patterns
            recognizers.Add(new Recognizer_InvertedHammer());
            recognizers.Add(new Recognizer_BullishInvertedHammer());
            recognizers.Add(new Recognizer_BearishInvertedHammer());

            // engulfing patterns - 2-candle patterns
            recognizers.Add(new Recognizer_Engulfing());
            recognizers.Add(new Recognizer_BullishEngulfing());
            recognizers.Add(new Recognizer_BearishEngulfing());

            // harami patterns - 2-candle patterns
            recognizers.Add(new Recognizer_Harami());
            recognizers.Add(new Recognizer_BullishHarami());
            recognizers.Add(new Recognizer_BearishHarami());
        }

        /// <summary>
        /// Gets all the pattern names - same as the property but as a method
        /// </summary>
        /// <returns>List of pattern names</returns>
        public List<string> GetPatternNames()
        {
            return recognizers.Select(r => r.patternName).ToList();
        }

        /// <summary>
        /// Looks for a specific pattern in a list of candlesticks
        /// Returns the indices where the pattern was found
        /// </summary>
        /// <param name="candlesticks">Candlesticks to search</param>
        /// <param name="patternName">Which pattern to look for</param>
        /// <returns>List of indices where pattern was found</returns>
        public List<int> RecognizePattern(List<aSmartCandlestick> candlesticks, string patternName)
        {
            // this will hold the indices where we find matches
            List<int> matchingIndices = new List<int>();

            // can't do anything without data
            if (candlesticks == null || candlesticks.Count == 0)
            {
                return matchingIndices;
            }

            // find the recognizer for this pattern
            Recognizer recognizer = GetRecognizerByName(patternName);

            // if we don't have a recognizer for this, return empty
            if (recognizer == null)
            {
                return matchingIndices;
            }

            // go through each candlestick and check for the pattern
            for (int i = 0; i < candlesticks.Count; i++)
            {
                // if pattern found at this index, add it to results
                if (recognizer.Recognize(candlesticks, i))
                {
                    matchingIndices.Add(i);
                }
            }

            return matchingIndices;
        }

        /// <summary>
        /// Runs all recognizers and returns all matches
        /// Good for comprehensive analysis
        /// </summary>
        /// <param name="candlesticks">Candlesticks to analyze</param>
        /// <returns>Dictionary mapping pattern names to list of matching indices</returns>
        public Dictionary<string, List<int>> RecognizeAllPatterns(List<aSmartCandlestick> candlesticks)
        {
            // dictionary to hold all results
            Dictionary<string, List<int>> allMatches = new Dictionary<string, List<int>>();

            // need data to work with
            if (candlesticks == null || candlesticks.Count == 0)
            {
                return allMatches;
            }

            // run each recognizer
            foreach (Recognizer recognizer in recognizers)
            {
                List<int> matchingIndices = new List<int>();

                // check each candlestick
                for (int i = 0; i < candlesticks.Count; i++)
                {
                    if (recognizer.Recognize(candlesticks, i))
                    {
                        matchingIndices.Add(i);
                    }
                }

                // add to dictionary
                allMatches[recognizer.patternName] = matchingIndices;
            }

            return allMatches;
        }

        /// <summary>
        /// Gets all patterns found at a specific index
        /// </summary>
        /// <param name="candlesticks">Candlesticks to check</param>
        /// <param name="index">Which index to look at</param>
        /// <returns>List of pattern names found at that index</returns>
        public List<string> GetPatternsAtIndex(List<aSmartCandlestick> candlesticks, int index)
        {
            List<string> matchingPatterns = new List<string>();

            // need data
            if (candlesticks == null || candlesticks.Count == 0)
            {
                return matchingPatterns;
            }

            // check index bounds
            if (index < 0 || index >= candlesticks.Count)
            {
                return matchingPatterns;
            }

            // check each recognizer at this index
            foreach (Recognizer recognizer in recognizers)
            {
                if (recognizer.Recognize(candlesticks, index))
                {
                    matchingPatterns.Add(recognizer.patternName);
                }
            }

            return matchingPatterns;
        }

        /// <summary>
        /// Finds a recognizer by its pattern name
        /// </summary>
        /// <param name="patternName">Name to search for</param>
        /// <returns>The recognizer, or null if not found</returns>
        public Recognizer GetRecognizerByName(string patternName)
        {
            // can't find anything without a name
            if (string.IsNullOrEmpty(patternName))
            {
                return null;
            }

            // find first recognizer with matching name (case insensitive)
            return recognizers.FirstOrDefault(r => 
                string.Equals(r.patternName, patternName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Gets recognizers that have a specific pattern size
        /// </summary>
        /// <param name="size">1 for single candle, 2 for two candle patterns</param>
        /// <returns>List of matching recognizers</returns>
        public List<Recognizer> GetRecognizersBySize(int size)
        {
            return recognizers.Where(r => r.patternSize == size).ToList();
        }

        /// <summary>
        /// Gets all the single-candlestick pattern recognizers
        /// </summary>
        /// <returns>List of 1-candle pattern recognizers</returns>
        public List<Recognizer> GetSingleCandlestickRecognizers()
        {
            return GetRecognizersBySize(1);
        }

        /// <summary>
        /// Gets all the two-candlestick pattern recognizers
        /// </summary>
        /// <returns>List of 2-candle pattern recognizers</returns>
        public List<Recognizer> GetTwoCandlestickRecognizers()
        {
            return GetRecognizersBySize(2);
        }
    }
}
