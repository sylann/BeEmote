using Newtonsoft.Json.Linq;

namespace BeEmote.Core
{
    /// <summary>
    /// Is able to instantiate Configurations for Cognitive API requests.
    /// </summary>
    public interface IConfigurable
    {

        /// <summary>
        /// Produce a new <see cref="RequestConfiguration"/> from the provided imagePath.
        /// Handles both local image path and remote url.
        /// </summary>
        /// <param name="imagePath">The path of the image (local or url)</param>
        /// <returns>A valid emotion request configuration or null</returns>
        RequestConfiguration GetEmotionConfiguration(string ImagePath);

        /// <summary>
        /// Produce a new <see cref="RequestConfiguration"/>
        /// from the provided <paramref name="query"/>, <paramref name="text"/> and <paramref name="language"/>.
        /// The resulting configuration depends on the value of <paramref name="query"/>
        /// </summary>
        /// <param name="query">The end of the Text Analytics API GET route</param>
        /// <param name="text">The text to analyse</param>
        /// <param name="language">The language of the text (may be null)</param>
        RequestConfiguration GetTextAnalyticsConfiguration(string query, string text, string language);
    }
}