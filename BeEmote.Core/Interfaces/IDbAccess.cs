using System.Collections.Generic;

namespace BeEmote.Core
{
    public interface IDbAccess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="faces"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        bool UpdateEmotion(List<Face> faces, string imagePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="sentiment"></param>
        /// <param name="textContent"></param>
        /// <returns></returns>
        bool UpdateTextAnalytics(Language language, double? sentiment, string textContent);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        EmotionStats GetEmotionStats();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        TextAnalyticsStats GetTextAnalyticsStats();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="facesCount"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        int InsertImgAnalysis(int facesCount, string imagePath);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="faces"></param>
        /// <param name="idImg"></param>
        /// <returns></returns>
        int InsertEmotion(List<Face> faces, int idImg);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="language"></param>
        /// <param name="sentiment"></param>
        /// <param name="textContent"></param>
        /// <returns></returns>
        int InsertTextAnalysis(Language language, double? sentiment, string textContent);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        double? GetImgAverageCallsPerDay();

        ///<summary>
        ///
        ///</summary>
        double? GetAverageFaceCount();

        ///<summary>
        ///
        ///</summary>
        /// <returns></returns>
        List<EmotionRank> GetDominantRanking();

        ///<summary>
        ///
        ///</summary>
        /// <returns></returns>
        double? GetTextAverageCallsPerDay();

        ///<summary>
        ///
        ///</summary>
        /// <returns></returns>
        List<LanguageRank> GetLanguageRanking();

        ///<summary>
        ///
        ///</summary>
        /// <returns></returns>
        List<SentimentRank> GetSentimentDistribution();
    }
}
