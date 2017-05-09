using System.Threading.Tasks;

namespace BeEmote.Core
{
    /// <summary>
    /// Is able to send an Http request.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Send a request to a Microsoft's cognitive API,
        /// according to the provided <paramref name="configuration"/>.
        /// Await for the response before returning it.
        /// </summary>
        /// <param name="configuration">Provides the url, the content and its type, and a credential key</param>
        /// <returns>Task(string) JSON data about the image</returns>
        Task<string> MakeRequest(RequestConfiguration conf);
    }
}
