using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace BeEmote.Core
{
    public interface ITextAnalyticsParser
    {
        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        JObject GetTextAnalyticsJson(string text);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="text"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        JObject GetTextAnalyticsJson(string text, string language);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        Language GetLanguageFromJsonResponse(string jsonString);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        List<string> GetKeyPhrasesFromJsonResponse(string jsonString);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        double? GetScoreFromJsonResponse(string jsonString);
    }
}
