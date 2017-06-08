using System.Collections.Generic;

namespace BeEmote.Core
{
    /// <summary>
    /// Contains the properties corresponding to the statistics
    /// that can be acquired through database requests.
    /// </summary>
    public class TextAnalyticsStats
    {
        /// <summary>
        /// Average number of calls to the TextAnalytics API.
        /// </summary>
        public double? AverageCallsPerDay { get; set; }

        /// <summary>
        /// Correspondence of a language name and its proportion in the database.
        /// </summary>
        public List<LanguageRank> LanguageRanking { get; set; }

        /// <summary>
        /// Correspondence of a sentiment rank and its number of
        /// occurrences in the database.
        /// </summary>
        public List<SentimentRank> SentimentDistribution { get; set; }
    }
}
