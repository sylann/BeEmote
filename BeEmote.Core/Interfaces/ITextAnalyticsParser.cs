using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BeEmote.Core
{
    public interface ITextAnalyticsParser
    {
        /// <summary>
        /// Serialize the provided text into a valid
        /// <see cref="JObject"/> having only one property.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        JObject GetTextAnalyticsJson(string text);

        /// <summary>
        /// Serialize the provided text and language into a valid
        /// <see cref="JObject"/> having 2 properties.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        JObject GetTextAnalyticsJson(string text, string language);

        /// <summary>
        /// Deserialize the <paramref name="jsonString"/>
        /// and retrieve a Language from it.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        Language GetLanguageFromJson(string jsonString);

        /// <summary>
        /// Deserialize the <paramref name="jsonString"/>
        /// and retrieve a List of key phrases from it.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        List<string> GetKeyPhrasesFromJson(string jsonString);

        /// <summary>
        /// Deserialize the <paramref name="jsonString"/>
        /// and retrieve a Score from it.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        double? GetScoreFromJson(string jsonString);
    }
}
