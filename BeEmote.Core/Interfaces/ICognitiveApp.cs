using System.Threading.Tasks;

namespace BeEmote.Core
{
    /// <summary>
    /// Provides methods to interact with the 2 Microsoft's Cognitive APIs:
    /// Emotion and TextAnalytics.
    /// </summary>
    public interface ICognitiveApp
    {
        /// <summary>
        /// Executes the complete logic from image encapsulation
        /// into a valid Emotion request, to its result handeling.
        /// </summary>
        /// <returns></returns>
        Task StartEmotion();

        /// <summary>
        /// Executes the complete logic from text encapsulation
        /// into 3 valid TextAnalytics requests, to their result handeling.
        /// </summary>
        /// <returns></returns>
        Task StartTextAnalytics();
    }
}
