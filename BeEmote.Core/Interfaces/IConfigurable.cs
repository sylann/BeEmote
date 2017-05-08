using Newtonsoft.Json.Linq;

namespace BeEmote.Core
{
    /// <summary>
    /// Is able to instantiate Configurations for Cognitive API requests.
    /// </summary>
    public interface IConfigurable
    {
        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="ImagePath"></param>
        /// <returns></returns>
        RequestConfiguration GetEmotionConfiguration(string ImagePath);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="ImageUrl"></param>
        /// <returns></returns>
        RequestConfiguration GetEmotionConfiguration(JObject ImageUrl);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="Query"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        RequestConfiguration GetTextAnalyticsConfiguration(string Query, JObject body);
    }
}