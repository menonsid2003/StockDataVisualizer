using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAnalysisProject
{
    internal class Candlestick
    {
        // all variables for a candlestick
        public decimal open { get; set; }
        public decimal high { get; set; }
        public decimal low { get; set; }
        public decimal close { get; set; }
        public long volume { get; set; }
        public DateTime date { get; set; }


        // constructor reading in data from csv
        public Candlestick(string rowofData)
        {
            // characters to interpret as seperators in input csv
            char[] separators = new char[] { ',', ' ', '"' };

            // split string based on seperators array
            string[] subs = rowofData.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            // pull date from the first element of each row of data
            string dateString = subs[0]; ;

            // convert input date into proper DateTime formatting
            date = DateTime.Parse(dateString);

            // variable to hold info until existence confirm
            decimal temp;

            // Open
            bool success = decimal.TryParse(subs[1], out temp);
            if (success) open = temp;

            // High
            success = decimal.TryParse(subs[2], out temp);
            if (success) high = temp;

            // Low
            success = decimal.TryParse(subs[3], out temp);
            if (success) low = temp;

            // Close
            success = decimal.TryParse(subs[4], out temp);
            if (success) close = temp;

            // subs[5] is adj. close which is unneeded so skip to subs[6]
            // Volume
            ulong tempVolume;
            success = ulong.TryParse(subs[6], out tempVolume);
            if (success) volume = (long)tempVolume;
        }
    }
    
    internal class SmartCandlestick : Candlestick
    {
        // Extra properties of a Smart Candlestick.

        // Range
        public decimal Range { get; set; } // Range of the candlestick (difference between high and low)
        public decimal BodyRange { get; set; } // Range of the body (difference between open and close)
        public decimal TopPrice { get; set; } // Highest price (maximum of open and close)
        public decimal BottomPrice { get; set; } // Lowest price (minimum of open and close)
        public decimal UpperTail { get; set; } // Length of the upper tail (difference between high and maximum of open and close)
        public decimal LowerTail { get; set; } // Length of the lower tail (difference between minimum of open and close and low)
        public Dictionary<string, bool> Patterns { get; private set; } // Dictionary to store detected patterns

        // Constructor that initializes properties and computes additional properties
        public SmartCandlestick(string rowofData) : base(rowofData)
        {
            calcExtraProperties(); // Calculate additional properties
            detectPatterns(); // Compute candlestick patterns
        }

        // Method to compute additional properties of the candlestick
        public void calcExtraProperties()
        {
            Range = Math.Abs(high - low); // Compute range
            BodyRange = Math.Abs(open - close); // Compute body range
            TopPrice = Math.Max(open, close); // Compute top price
            BottomPrice = Math.Min(open, close); // Compute bottom price
            UpperTail = high - Math.Max(open, close); // Compute upper tail
            LowerTail = Math.Min(open, close) - low; // Compute lower tail
        }

        // Method to detect candlestick patterns
        private void detectPatterns()
        {
            // Initialize the dictionary to store detected patterns
            Patterns = new Dictionary<string, bool>();

            // Bullish pattern: Close price is higher than open price
            bool isBullish = (close > open);
            Patterns["Bullish"] = isBullish;

            // Bearish pattern: Open price is higher than close price
            bool isBearish = (open > close);
            Patterns["Bearish"] = isBearish;

            // Neutral pattern: Open price is equal to close price
            bool isNeutral = (open == close);
            Patterns["Neutral"] = isNeutral;

            // Marubozu pattern: The candlestick has no upper or lower shadows, or the range is at least twice the body range
            // To avoid divide by zero errors, ensure BodyRange is not exactly 0.
            bool isMarubozu = (BodyRange != 0 && (Range / BodyRange) >= 2);
            Patterns["Marubozu"] = isMarubozu;

            // Hammer pattern: The lower tail is at least twice the size of the body, and the upper tail is small or nonexistent
            bool isHammer = (LowerTail >= (2 * BodyRange) && UpperTail <= (BodyRange / 2));
            Patterns["Hammer"] = isHammer;

            // Doji pattern: Close price is very close to open price (within a small threshold)
            bool isDoji = (Math.Abs((double)(close - open)) <= 0.05 * (double)Range); // Adjust threshold as needed
            Patterns["Doji"] = isDoji;


            decimal threshold = 0.1m; // 10% threshold for significant difference

            // Mean used for calculation of closeness only
            decimal avgPrice = (open + high + close) / 3;

            // Calculate the percentage difference between low price and average price
            decimal percentageDifference = Math.Abs((low - avgPrice) / avgPrice) * 100;

            // Check if the percentage difference exceeds the threshold
            bool significantDiff = percentageDifference > threshold;

            // Dragonfly Doji pattern: Open, High, and Close prices equal or very close to each other,
            // while the Low price is significantly lower than the former three
            // 0.2 seemed to find the most dragonflys while still fitting the visual criteria.
            bool isDragonflyDoji = (Math.Abs((double)(open - high)) <= 0.2 * (double)Range) &&
                                  (Math.Abs((double)(high - close)) <= 0.2 * (double)Range) &&
                                  (Math.Abs((double)(open - close)) <= 0.2 * (double)Range) &&
                                  (significantDiff);

            Patterns["DragonflyDoji"] = isDragonflyDoji;


            // Mean used for calculation of closeness only
            avgPrice = (open + low + close) / 3;

            // Calculate the percentage difference between low price and average price
            percentageDifference = Math.Abs((high - avgPrice) / avgPrice) * 100;

            // Check if the percentage difference exceeds the threshold
            significantDiff = percentageDifference > threshold;

            // Gravestone Doji pattern: Open, Close, and Low prices equal or very close to each other,
            // while the High price is significantly higher than the former three
            // 0.2 seemed to find the most gravestones while still fitting the visual criteria.
            bool isGravestoneDoji = (Math.Abs((double)(open - low)) <= 0.2 * (double)Range) &&
                                    (Math.Abs((double)(low - close)) <= 0.2 * (double)Range) &&
                                    (Math.Abs((double)(open - close)) <= 0.2 * (double)Range) &&
                                    (significantDiff);

            Patterns["GravestoneDoji"] = isGravestoneDoji;
        }
    }
}
