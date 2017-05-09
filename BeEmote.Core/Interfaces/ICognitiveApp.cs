using System.Threading.Tasks;

namespace BeEmote.Core
{
    /// <summary>
    /// Defines the methods that are mandatory to interact 
    /// with the 2 following Microsoft's Cognitive APIs:
    /// Emotion and TextAnalytics.
    /// </summary>
    public interface ICognitiveApp<T>
    {
        /// <summary>
        /// Contains the model for a typical Microsoft's Cognitive API Response.
        /// </summary>
        T Response { get; set; }
        /// <summary>
        /// Indicates the state in which the app currently is
        /// </summary>
        RequestStates State { get; set; }
        /// <summary>
        /// Executes the complete logic from image encapsulation
        /// into a valid Emotion request, to its result handeling.
        /// </summary>
        Task Start();
        /// <summary>
        /// Reset the <see cref="State"/> to its default value.
        /// Should be executed between each call to <see cref="Start"/>.
        /// </summary>
        /// <returns>The resulting state</returns>
        RequestStates Reset();
        /// <summary>
        /// Awaits and returns the result of a configured
        /// request to a Microsoft's Cognitive API.
        /// </summary>
        /// <param name="config">A configuration for the request</param>
        /// <returns>json response of the API</returns>
        Task<string> SendRequest(RequestConfiguration config);
    }
}
