using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeEmote.Core
{
    public interface ITextAnalyticsAPI
    {
        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<Language> GetTextAnalyticsLanguage(RequestConfiguration conf);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<List<string>> GetTextAnalyticsKeyPhrases(RequestConfiguration conf);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<double?> GetTextAnalyticsScore(RequestConfiguration conf);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="query"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        RequestConfiguration GetTextAnalyticsConfiguration(string query, string text);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="query"></param>
        /// <param name="text"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        RequestConfiguration GetTextAnalyticsConfiguration(string query, string text, string language);
    }
}
