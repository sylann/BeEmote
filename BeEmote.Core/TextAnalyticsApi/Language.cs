using System;

namespace BeEmote.Core
{
    /// <summary>
    /// Informations on the language that can be detected in a text by the Microsoft's Text Analytics API.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Name of the language
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This is the 2 character norm notation corresponding to the laguage Name.
        /// </summary>
        public string Iso6391Name { get; set; }

        /// <summary>
        /// The score represents the confidence in the language detection
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// The confidence score of the language, formatted as a percentage.
        /// </summary>
        public string Confidence { get => $"{Math.Round(Score * 100, 2)}%"; }
    }
}
