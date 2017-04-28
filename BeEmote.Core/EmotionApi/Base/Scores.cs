using System;

namespace BeEmote.Core
{
    /// <summary>
    /// This class corresponds to a JSON structure as defined by the Microsoft's Emotion API.
    /// It provides the score for each valid <see cref="Emotions"/>
    /// The JSON.net Deserializer automatically sets the public properties.
    /// </summary>
    public class Scores
    {
        #region Main Public Properties

        public double Anger { get; set; }
        public double Contempt { get; set; }
        public double Disgust { get; set; }
        public double Fear { get; set; }
        public double Happiness { get; set; }
        public double Neutral { get; set; }
        public double Sadness { get; set; }
        public double Surprise { get; set; }

        #endregion

        #region Decorative Public Properties

        public string AngerHR { get => HumanReadable(Anger); }
        public string ContemptHR { get => HumanReadable(Contempt); }
        public string DisgustHR { get => HumanReadable(Disgust); }
        public string FearHR { get => HumanReadable(Fear); }
        public string HappinessHR { get => HumanReadable(Happiness); }
        public string NeutralHR { get => HumanReadable(Neutral); }
        public string SadnessHR { get => HumanReadable(Sadness); }
        public string SurpriseHR { get => HumanReadable(Surprise); }

        #endregion
        
        #region Private Methods

        private string HumanReadable(double score) => $"{Math.Round(score * 100, 2)}%";

        #endregion
    }
}
