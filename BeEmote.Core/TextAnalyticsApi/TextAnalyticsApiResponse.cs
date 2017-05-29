using System;
using System.Collections.Generic;
using System.Linq;

namespace BeEmote.Core
{
    /// <summary>
    /// Information resulting from sending a text request to the Microsoft's Text Analytics API.
    /// It concists of a detected <see cref="Core.Language"/>, a list of key phrases depending on the language
    /// and a score of sentiment for the overall text (depending on the language).
    /// </summary>
    public class TextAnalyticsApiResponse : IDescribable
    {
        #region Public Properties

        /// <summary>
        /// The detected language of the text. Only one can be detected.
        /// </summary>
        public Language Language { get; set; }

        /// <summary>
        /// The key phrases found in the text are the expressions
        /// transporting the most part of the sens in the text.
        /// Requires that the language be known. Not every languages are supported.
        /// </summary>
        public List<string> KeyPhrases { get; set; }

        /// <summary>
        /// The sentiment score for the analysed text.
        /// A higher value means the text feels more positive and reciprocally.
        /// Requires that the language be known. Not every languages are supported.
        /// </summary>
        public double? Score { get; set; }

        /// <summary>
        /// The list of key phrases formatted as a single string.
        /// Key phrases are separated by comma.
        /// </summary>
        public string Sentiment { get => (Score == null) ? "-" : $"{Math.Round((double)Score * 100, 2)}%"; }

        /// <summary>
        /// The list of key phrases formatted as a single string.
        /// Key phrases are separated by comma.
        /// </summary>
        public string FormattedLanguage
        {
            get => Language is null
                ? "Unknown language"
                : $"{Language.Name}[{Language.Iso6391Name}] ({Language.Confidence})";
        }

        /// <summary>
        /// The list of key phrases formatted as a single string.
        /// Key phrases are separated by comma.
        /// </summary>
        public string FormattedKeyPhrases { get => KeyPhrases?.Aggregate((result, k) => result == null ? $"'{k}'" : $"{result}, '{k}'"); }

        /// <summary>
        /// Gets the number of key phrases (returns 0 if the list is null or empty)
        /// </summary>
        public int NbKeyPhrases { get => KeyPhrases?.Count ?? 0; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prints an easily readable recap of the result in the Console.
        /// </summary>
        public void Describe()
        {
            Console.WriteLine($"\n==========================\nText Analytics API Result:\n");
            Console.WriteLine($"Language detected: {FormattedLanguage})");
            Console.WriteLine($"Sentiment: {Sentiment}");
            Console.WriteLine($"Key phrases:\n{FormattedKeyPhrases}");
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Formats the score into a readable percentage.
        /// </summary>

        #endregion
    }
}
