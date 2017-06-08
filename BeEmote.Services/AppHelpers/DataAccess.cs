﻿///<License terms GNU v3>
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
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
#region Main public methods

        /// <summary>
        /// Generates an entry in imganalysis table.
        /// Then insert each face's data into the emotion table,
        /// with the corresponding imganalysis id.
        /// </summary>
        /// <param name="faces">List of faces from the image</param>
        /// <returns>imgAnalysis AND emotion both succeeded?</returns>
        public bool UpdateEmotion(List<Face> faces, string imagePath)
        {
            var idImg = InsertImgAnalysis(faces.Count, imagePath);
            var newEmotionEntries = InsertEmotion(faces, idImg);

            return idImg != 0 && newEmotionEntries == faces.Count;
        }

        /// <summary>
        /// Generates an entry in textanalysis table.
        /// </summary>
        /// <returns>all procedures succeeded</returns>
        public bool UpdateTextAnalytics(Language language, double? sentiment, string textContent)
        {
            var idText = InsertTextAnalysis(language, sentiment, textContent);

            return idText != 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EmotionStats GetEmotionStats()
        {
            return new EmotionStats()
            {
                AverageCallsPerDay = GetImgAverageCallsPerDay(),
                AverageFaceCount = GetAverageFaceCount(),
                DominantRanking = GetDominantRanking()
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public TextAnalyticsStats GetTextAnalyticsStats()
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
        public int InsertImgAnalysis(int facesCount, string imagePath)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    int idImg = conn.Query<int>(DatabaseManager.InsertIntoImgAnalysis, new
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
        public int InsertEmotion(List<Face> faces, int idImg)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    Console.WriteLine($"DB pre-check: Number of faces in the image: {faces.Count}");
                    int newEmotionEntries = 0;
                    foreach (Face f in faces)
                    {
                        int idEmo = conn.Query<int>(DatabaseManager.InsertIntoEmotion, new
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
        public int InsertTextAnalysis(Language language, double? sentiment, string textContent)
        {
            try
            {
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    int idText = conn.Query<int>(DatabaseManager.InsertIntoTextAnalysis, new
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
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    return conn.Query<double>(DatabaseManager.ImgAverageCallsPerDay).SingleOrDefault();
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
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    return conn.Query<double>(DatabaseManager.AverageFaceCount).SingleOrDefault();
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
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    return conn.Query<EmotionRank>(DatabaseManager.DominantRanking).ToList();
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
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    return conn.Query<double?>(DatabaseManager.AverageCallsPerDayTextAnalysis).SingleOrDefault();
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
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    return conn.Query<LanguageRank>(DatabaseManager.LanguageDistribution).ToList();
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
                using (IDbConnection conn = new MySqlConnection(DatabaseManager.MySql_BeEmote))
                {
                    var temp = conn.Query<SentimentRank>(DatabaseManager.SentimentDistribution).ToList();
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
