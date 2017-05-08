using System.Threading.Tasks;

namespace BeEmote.Core
{
    public interface IEmotionAPI
    {
        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<EmotionApiResponse> GetEmotionFaces(RequestConfiguration conf);

        /// <summary>
        /// Todo: Add description
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        RequestConfiguration GetEmotionConfiguration(string imagePath);
    }
}
