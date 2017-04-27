using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Language Language { get; private set; }

        /// <summary>
        /// The key phrases found in the text are the expressions
        /// transporting the most part of the sens in the text.
        /// Requires that the language be known. Not every languages are supported.
        /// </summary>
        public List<string> KeyPhrases { get; private set; }

        /// <summary>
        /// The sentiment score for the analysed text.
        /// A higher value means the text feels more positive and reciprocally.
        /// Requires that the language be known. Not every languages are supported.
        /// </summary>
        public double Score { get; private set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Default constructor.
        /// Parse the detectedLanguages from the response body into the Language property.
        /// </summary>
        public TextAnalyticsApiResponse(string json)
        {
            // Phase 1: Get the language from the Text Analytics Cognitive service
            JObject raw = JObject.Parse(json);
            Language = raw["documents"][0]["detectedLanguages"][0].ToObject<Language>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public void UpdateKeyPhrases(string json)
        {
            // Phase 2: Get the Key Phrases from the Text Analytics Cognitive service
            JObject raw = JObject.Parse(json);
            KeyPhrases = raw["documents"][0]["keyPhrases"].ToObject<List<string>>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        public void UpdateScore(string json)
        {
            // Phase 3: Get the Sentiment from the Text Analytics Cognitive service
            JObject raw = JObject.Parse(json);
            Score = (double)raw["documents"][0]["score"];
        }

        #endregion
    }
}
