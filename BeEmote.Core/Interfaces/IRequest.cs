using System.Threading.Tasks;

namespace BeEmote.Core
{
    /// <summary>
    /// Is able to send an Http request.
    /// </summary>
    public interface IRequest
    {
        /// <summary>
        /// Send a request to a server with parameters depending on the configuration,
        /// Await for an async response.
        /// </summary>
        /// <param name="imageFilePath"></param>
        /// <returns>Task(string) JSON data about the image</returns>
        Task<string> MakeRequest(RequestConfiguration conf);
    }
}
