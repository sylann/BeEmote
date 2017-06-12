///<License terms GNU v3>
/// BeEmote is a simple application that allows you to analyse photos
/// or text with the Microsoft's Cognitive "Emotion API" and "Text Analytics API"
/// Copyright (C) 2017  Romain Vincent
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </License>

using BeEmote.Core;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeEmote.Services
{
    /// <summary>
    /// Contains all the logic of working with the database.
    /// Does not take care of the code,
    /// Does not know about procedures (relies on DBManager).
    /// </summary>
    public class DataAccess
    {
        MySqlConnectionFactory DB = new MySqlConnectionFactory();

        #region Main public methods

        /// <summary>
        /// Generates an entry in imganalysis table.
        /// Then insert each face's data into the emotion table,
        /// with the corresponding imganalysis id.
        /// </summary>
        /// <param name="faces">List of faces from the image</param>
        /// <returns>imgAnalysis AND emotion both succeeded?</returns>
        public virtual bool UpdateEmotion(List<Face> faces, string imagePath)
        {
            var idImg = InsertImgAnalysis(faces.Count, imagePath);
            var newEmotionEntries = InsertEmotion(faces, idImg);

            return idImg != 0 && newEmotionEntries == faces.Count;
        }

        /// <summary>
        /// Generates an entry in textanalysis table.
        /// </summary>
        /// <returns>all procedures succeeded</returns>
        public virtual bool UpdateTextAnalytics(Language language, double? sentiment, string textContent)
        {
            var idText = InsertTextAnalysis(language, sentiment, textContent);

            return idText != 0;
        }

        /// <summary>
        /// Returns an <see cref="EmotionStats"/> object with the following properties:
        ///   AverageCallsPerDay,
        ///   AverageFaceCount,
        ///   DominantRanking
        /// </summary>
        /// <returns></returns>
        public virtual EmotionStats GetEmotionStats()
        {
            return new EmotionStats()
            {
                AverageCallsPerDay = GetImgAverageCallsPerDay(),
                AverageFaceCount = GetAverageFaceCount(),
                DominantRanking = GetDominantRanking()
            };
        }

        /// <summary>
        /// Returns an <see cref="TextAnalyticsStats"/> object with the following properties:
        ///   AverageCallsPerDay,
        ///   LanguageRanking,
        ///   SentimentDistribution
        /// </summary>
        /// <returns></returns>
        public virtual TextAnalyticsStats GetTextAnalyticsStats()
        {
            return new TextAnalyticsStats()
            {
                AverageCallsPerDay = GetTextAverageCallsPerDay(),
                LanguageRanking = GetLanguageRanking(),
                SentimentDistribution = GetSentimentDistribution()
            };
        }

#endregion

#region Emotion Insertion Methods

        /// <summary>
        /// Executes the insertinto_imganalysis stored procedure.
        /// </summary>
        /// <param name="facesCount"></param>
        /// <returns></returns>
        public virtual int InsertImgAnalysis(int facesCount, string imagePath)
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    int idImg = conn.Query<int>(DatabaseProcedures.InsertIntoImgAnalysis, new
                        {
                            NbFaces   = facesCount,
                            ImagePath = imagePath
                        }).SingleOrDefault();
                    Console.WriteLine($"DB post-check: New entry in localhost.beemote.imganalysis: id={idImg}");
                    return idImg;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Executes the insertinto_emotion stored procedure.
        /// </summary>
        public virtual int InsertEmotion(List<Face> faces, int idImg)
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    Console.WriteLine($"DB pre-check: Number of faces in the image: {faces.Count}");
                    int newEmotionEntries = 0;
                    foreach (Face f in faces)
                    {
                        int idEmo = conn.Query<int>(DatabaseProcedures.InsertIntoEmotion, new
                        {
                            IdImg     = idImg,
                            Dominant  = f.GetDominantEmotion().ToString(),
                            RLeft     = f.FaceRectangle.Left,
                            RTop      = f.FaceRectangle.Top,
                            RWidth    = f.FaceRectangle.Width,
                            RHeight   = f.FaceRectangle.Height,
                            Anger     = f.Scores.Anger,
                            Contempt  = f.Scores.Contempt,
                            Disgust   = f.Scores.Disgust,
                            Fear      = f.Scores.Fear,
                            Happiness = f.Scores.Happiness,
                            Neutral   = f.Scores.Neutral,
                            Sadness   = f.Scores.Sadness,
                            Surprise  = f.Scores.Surprise
                        }).SingleOrDefault();
                        // increment only if a new id is returned from the database

                        if (idEmo != 0)
                            newEmotionEntries++;
                    }
                    Console.WriteLine($"DB post-check: {newEmotionEntries} new entry-ies in localhost.beemote.emotion");
                    return newEmotionEntries;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return 0;
            }
        }

#endregion

#region TextAnalytics Insertion Methods

        /// <summary>
        /// Executes the insertinto_textanalysis stored procedure.
        /// </summary>
        /// <param name="language"></param>
        /// <param name="sentiment"></param>
        /// <returns></returns>
        public virtual int InsertTextAnalysis(Language language, double? sentiment, string textContent)
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    int idText = conn.Query<int>(DatabaseProcedures.InsertIntoTextAnalysis, new
                    {
                        LangName = language.Name,
                        LangISO = language.Iso6391Name,
                        LangScore = language.Score,
                        TextScore = sentiment,
                        TextContent = textContent
                    }).SingleOrDefault();
                    Console.WriteLine($"DB post-check: New entry in localhost.beemote.textanalysis: id={idText}");
                    return idText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return 0;
            }
        }

#endregion

#region Emotion Select Methods
        
        /// <summary>
        /// Gets the average number of calls to the Emotion API
        /// (based on the number of entries in the imganalysis table)
        /// </summary>
        /// <returns>Average calls per day (nullable)</returns>
        public double? GetImgAverageCallsPerDay()
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    return conn.Query<double>(DatabaseProcedures.ImgAverageCallsPerDay).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public double? GetAverageFaceCount()
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    return conn.Query<double>(DatabaseProcedures.AverageFaceCount).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmotionRank> GetDominantRanking()
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    return conn.Query<EmotionRank>(DatabaseProcedures.DominantRanking).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return null;
            }
        }

#endregion

#region TextAnalytics Select Methods

        /// <summary>
        /// Gets the average number of calls to the Emotion API
        /// (based on the number of entries in the imganalysis table)
        /// </summary>
        /// <returns>Average calls per day (nullable)</returns>
        public double? GetTextAverageCallsPerDay()
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    return conn.Query<double?>(DatabaseProcedures.AverageCallsPerDayTextAnalysis).SingleOrDefault();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the proportion of each known language. Descending order.
        /// </summary>
        /// <returns></returns>
        public List<LanguageRank> GetLanguageRanking()
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    return conn.Query<LanguageRank>(DatabaseProcedures.LanguageDistribution).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Gets the number of Entries corresponding to each rank:
        ///   0.00 - 0.30
        ///   0.31 - 0.60
        ///   0.61 - 1.00
        /// </summary>
        /// <returns></returns>
        public List<SentimentRank> GetSentimentDistribution()
        {
            try
            {
                using (var conn = DB.CreateConnection())
                {
                    var temp = conn.Query<SentimentRank>(DatabaseProcedures.SentimentDistribution).ToList();
                    return temp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DB post-check: Connection failure:\n{ex.Message}");
                return null;
            }
        }

#endregion

    }
}
