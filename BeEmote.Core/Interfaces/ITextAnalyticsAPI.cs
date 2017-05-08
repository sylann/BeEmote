using BeEmote.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BeEmote.Services
{
    public interface ITextAnalyticsAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<Language> GetTextAnalyticsLanguage(RequestConfiguration conf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<List<string>> GetTextAnalyticsKeyPhrases(RequestConfiguration conf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<double?> GetTextAnalyticsScore(RequestConfiguration conf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        RequestConfiguration GetTextAnalyticsConfiguration(string query, string text);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="text"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        RequestConfiguration GetTextAnalyticsConfiguration(string query, string text, string language);
    }
}
