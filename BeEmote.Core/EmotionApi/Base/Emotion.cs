namespace BeEmote.Core
{
    /// <summary>
    /// An emotion that can be identified on a face in the Microsoft's Emotion API.
    /// See existing <see cref="Emotions"/>.
    /// </summary>
    public class Emotion
    {
        /// <summary>
        /// The name of the emotion
        /// </summary>
        public Emotions Name { get; }

        /// <summary>
        /// The score of the emotion as defined by the Emotion API
        /// </summary>
        public double Score { get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Score"></param>
        public Emotion(Emotions Name, double Score)
        {
            this.Name = Name;
            this.Score = Score;
        }
    }
}
