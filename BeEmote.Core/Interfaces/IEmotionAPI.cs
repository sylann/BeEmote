using BeEmote.Core;
using System.Threading.Tasks;

namespace BeEmote.Services
{
    public interface IEmotionAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conf"></param>
        /// <returns></returns>
        Task<EmotionApiResponse> GetEmotionFaces(RequestConfiguration conf);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        RequestConfiguration GetEmotionConfiguration(string imagePath);
    }
}
