namespace BeEmote.Core
{
    /// <summary>
    /// Associates an emotion name to its count of occurrences in the database
    /// </summary>
    public class EmotionRank
    {
        /// <summary>
        /// The emotion name.
        /// </summary>
        public Emotions Dominant { get; set; }

        /// <summary>
        /// The count of occurrences in the database.
        /// </summary>
        public int Count { get; set; }
    }
}
