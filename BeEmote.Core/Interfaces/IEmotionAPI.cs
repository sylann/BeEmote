using System.Threading.Tasks;

namespace BeEmote.Core
{
    /// <summary>
    /// Defines the methods that are mandatory to interact 
    /// with the Emotion API in particular.
    /// </summary>
    public interface IEmotionAPI
    {
        /// <summary>
        /// Calls the <see cref="RequestManager.GetEmotionConfiguration(string)"/> method.
        /// imagePath can be either a local path or a remote url.
        /// </summary>
        /// <param name="imagePath">The path of the image to analyse</param>
        /// <returns>A configuration for an Emotion API Request</returns>
        RequestConfiguration Configure(string imagePath);
    }
}
