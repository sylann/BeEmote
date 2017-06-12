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

namespace BeEmote.Services
{
    /// <summary>
    /// Contains constant strings to use together with the Dapper package.
    /// Allows execution of Stored procedures of a MySql database.
    /// </summary>
    public class DatabaseProcedures
    {
        public const string InsertIntoImgAnalysis = "insertinto_imganalysis(@NbFaces,@ImagePath)";
        public const string InsertIntoEmotion = "insertinto_emotion(@IdImg,@Dominant,@RLeft,@RTop,@RWidth,@RHeight,@Anger,@Contempt,@Disgust,@Fear,@Happiness,@Neutral,@Sadness,@Surprise)";
        public const string InsertIntoTextAnalysis = "insertinto_textanalysis(@LangName,@LangISO,@LangScore,@TextScore,@TextContent)";
        public const string ImgAverageCallsPerDay = "averagecallsperday_emotion()";
        public const string AverageFaceCount = "averagefacecount_emotion()";
        public const string DominantRanking = "dominantdistribution_emotion()";
        public const string AverageCallsPerDayTextAnalysis = "averagecallsperday_textanalysis()";
        public const string LanguageDistribution = "languagedistribution_textanalysis()";
        public const string SentimentDistribution = "sentimentdistribution_textanalysis()";
    }
}
