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
    public class TextAnalyticsApiResponse
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
        public double Score { get; set; }

        #endregion

        #region Public Methods


        public void Describe()
        {
            Console.WriteLine($"\n==========================\nText Analytics API Result:\n");
            Console.WriteLine($"Language detected: {Language.Name}[{Language.Iso6391Name}] ({HumanReadable(Language.Score)})");
            Console.WriteLine($"Sentiment: {HumanReadable(Score)}");
            Console.WriteLine($"Key phrases:\n{KeyPhrases.Aggregate((result, phrase) => result == null ? $"'{phrase}'" : $"{result}, '{phrase}'")}");
        }

        #endregion

        #region Private Methods

        private string HumanReadable(double score) => $"{Math.Round(score * 100, 2)}%";

        #endregion
    }
}
