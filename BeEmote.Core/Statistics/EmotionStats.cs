using System.Collections.Generic;
using System.ComponentModel;

namespace BeEmote.Core
{
    /// <summary>
    /// Contains the properties corresponding to the statistics
    /// that can be acquired through database requests.
    /// </summary>
    public class EmotionStats
    {
        /// <summary>
        /// Average number of calls to the Emotion API.
        /// </summary>
        public double? AverageCallsPerDay { get; set; }

        /// <summary>
        /// Average number of faces detected per image.
        /// </summary>
        public double? AverageFaceCount { get; set; }

        /// <summary>
        /// List of correspondence of an emotion name and its number of
        /// occurrences as a dominant emotion in the database.
        /// </summary>
        public List<EmotionRank> DominantRanking { get; set; }

        public override string ToString()
        {
            var text = $"Average calls per day: {AverageCallsPerDay}\nAverage face count: {AverageFaceCount}\nDominantRanking:\n";
            DominantRanking.ForEach(x => text += $"- {x.ToString()}\n");
            return text;
        }
    }
}
