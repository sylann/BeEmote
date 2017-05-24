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

using System.Configuration;

namespace BeEmote.Services
{
    /// <summary>
    /// Kind of interface for the database.
    /// Contains everything that directly concerns the connection to the database
    /// As well as everything that identifies names from the database.
    /// </summary>
    public class DatabaseManager
    {
        #region Connection String

        /// <summary>
        /// Helper method that provides a complete connection string
        /// from the provided partial connection string
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string Connect(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Partial connection string
        /// </summary>
        public static string MySql_BeEmote { get => Connect("Mysql_BeEmote"); }

        #endregion

        #region Stored Procedures INSERT INTO

        /// <summary>
        /// Stored procedure signature INSERT INTO imganalysis
        /// </summary>
        public static string InsertIntoImgAnalysis { get => "insertinto_imganalysis(@NbFaces)"; }

        /// <summary>
        /// Stored procedure signature INSERT INTO emotion
        /// </summary>
        public static string InsertIntoEmotion { get => "insertinto_emotion(@IdImg,@Dominant,@RLeft,@RTop,@RWidth,@RHeight,@Anger,@Contempt,@Disgust,@Fear,@Happiness,@Neutral,@Sadness,@Surprise)"; }

        /// <summary>
        /// Stored procedure signature INSERT INTO textanalysis
        /// </summary>
        public static string InsertIntoTextAnalysis { get => "insertinto_textanalysis(@LangName,@LangISO,@LangScore,@TextScore)"; }

        #endregion
    }
}
