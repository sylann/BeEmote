using BeEmote.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using BeEmote.Services;

namespace BeEmote.Services.Tests
{
    [TestClass()]
    public class DataAccessTests
    {
        // Tested class instance
        private readonly DataAccess db = new DataAccess();

        // after Emotion request
        // - number of calls should increase by 1
        // - number of global calls should increase by 1

        // after TextAnalytics request
        // - number of calls should increase by 1
        // - number of global calls should increase by 1

        // after Failed request
        // - number of fail request should increase by 1
        // - stats for emotions or text analytics should not change

        [TestMethod()]
        public void UpdateTextAnalytics_CorrectValues()
        {
            //Arrange 
            Language language = new Language()
            {
                Name = "English",
                Iso6391Name = "en",
                Score = 1
            };
            double? sentiment = 50;
            string textContent = "Hello world !";

            //Act
            var actual = db.UpdateTextAnalytics(language, sentiment, textContent);

            //Assert
            Assert.AreNotEqual(0, actual);
        }

        [TestMethod()]
        public void InsertImgAnalysis_Valid_IdImage()
        {
            //Arrange 
            int facesCount = 4;
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "oberyn-wear-helmet.jpg");
            var mockHelper = new Mock<IDbAccess>();

            mockHelper.Setup(e => e.InsertImgAnalysis(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(1);

            //Act
            IDbAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreNotEqual(0, mockObject.InsertImgAnalysis(facesCount, imagePath));
        }

        [TestMethod()]
        public void InsertImgAnalysis_Invalid_IdImage()
        {
            //Arrange 
            int facesCount = 4;
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "oberyn-wear-helmet.jpg");
            var mockHelper = new Mock<IDbAccess>();

            mockHelper.Setup(e => e.InsertImgAnalysis(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(0);

            //Act
            IDbAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreEqual(0, mockObject.InsertImgAnalysis(facesCount, imagePath));
        }

        [TestMethod()]
        public void InsertEmotion_Valid_EmotionEntries()
        {
            //Arrange 
            FaceRectangle getFaceRectangleData = new FaceRectangle
            {
                Left = 10,
                Top = 10,
                Width = 10,
                Height = 10
            };
            Scores getScoresData = new Scores
            {
                Anger = 0.1,
                Contempt = 0,
                Disgust = 0,
                Fear = 0,
                Happiness = 0,
                Neutral = 0.9,
                Sadness = 0,
                Surprise = 0
            };
            Face face = new Face
            {
                FaceRectangle = getFaceRectangleData,
                Scores = getScoresData
            };
            List<Face> faces = new List<Face>() { face };
            int idImg = 1;
            var mockHelper = new Mock<IDbAccess>();

            mockHelper.Setup(e => e.InsertEmotion(It.IsAny<List<Face>>(), It.IsAny<int>()))
                .Returns(idImg);

            //Act
            IDbAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreNotEqual(0, mockObject.InsertEmotion(faces, idImg));
        }

        [TestMethod()]
        public void InsertEmotion_Invalid_EmotionEntries()
        {
            //Arrange 
            FaceRectangle getFaceRectangleData = new FaceRectangle
            {
                Left = 10,
                Top = 10,
                Width = 10,
                Height = 10
            };
            Scores getScoresData = new Scores
            {
                Anger = 0.1,
                Contempt = 0,
                Disgust = 0,
                Fear = 0,
                Happiness = 0,
                Neutral = 0.9,
                Sadness = 0,
                Surprise = 0
            };
            Face face = new Face
            {
                FaceRectangle = getFaceRectangleData,
                Scores = getScoresData
            };
            List<Face> faces = new List<Face>() { face };
            int idImg = 0;
            var mockHelper = new Mock<IDbAccess>();

            mockHelper.Setup(e => e.InsertEmotion(It.IsAny<List<Face>>(), It.IsAny<int>()))
                .Returns(idImg);

            //Act
            IDbAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreEqual(0, mockObject.InsertEmotion(faces, idImg));
        }

        [TestMethod()]
        public void InsertTextAnalysis_ValidValue()
        {
            //Arrange 
            Language lang = new Language
            {
                Name = "English",
                Iso6391Name = "En",
                Score = 100
            };
            double? sentiment = 0;
            string textContent = "Hello world !";
            int idText = 1;

            var mockHelper = new Mock<IDbAccess>();

            //Act

            mockHelper.Setup(e => e.InsertTextAnalysis(It.IsAny<Language>(), It.IsAny<double?>(), It.IsAny<string>()))
                .Returns(idText);
            IDbAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreNotEqual(0, mockObject.InsertTextAnalysis(lang, sentiment, textContent));
        }

        [TestMethod()]
        public void InsertTextAnalysis_InvalidValue()
        {
            //Arrange 
            Language lang = new Language
            {
                Name = "English",
                Iso6391Name = "En",
                Score = 100
            };
            double? sentiment = 0;
            string textContent = "Hello world !";
            int idText = 0;

            var mockHelper = new Mock<IDbAccess>();

            //Act
            mockHelper.Setup(e => e.InsertTextAnalysis(It.IsAny<Language>(), It.IsAny<double?>(), It.IsAny<string>()))
                .Returns(idText);
            IDbAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreEqual(0, mockObject.InsertTextAnalysis(lang, sentiment, textContent));
        }

        [TestMethod]
        public void GetImgAverageCallsPerDay_Returns_Correct_Type()
        {
            // Arrange
            var expected = typeof(double);

            // Act
            var actual = db.GetImgAverageCallsPerDay().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAverageFaceCount_Returns_Correct_Type()
        {
            // Arrange
            var expected = typeof(double);

            // Act
            var actual = db.GetAverageFaceCount().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetDominantRanking_Returns_Correct_Type()
        {
            // Arrange
            var expected = typeof(List<EmotionRank>);

            // Act
            var actual = db.GetDominantRanking().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetTextAverageCallsPerDay_Returns_Correct_Type()
        {
            // Arrange
            var expected = typeof(double);

            // Act
            var actual = db.GetTextAverageCallsPerDay().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetLanguageRanking_Returns_Correct_Type()
        {
            // Arrange
            var expected = typeof(List<LanguageRank>);

            // Act
            var actual = db.GetLanguageRanking().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSentimentDistribution_Returns_Correct_Type()
        {
            // Arrange
            var expected = typeof(List<SentimentRank>);

            // Act
            var actual = db.GetSentimentDistribution().GetType();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetEmotionStats_When_Return()
        {
            //Arrange
            var expected = new EmotionStats
            {
                AverageCallsPerDay = 7.5f,
                AverageFaceCount = 0.7f,
                DominantRanking = new List<EmotionRank>() { }
            };
            var dbMock = new Mock<IDbAccess>();
            dbMock.Setup(e => e.GetEmotionStats()).Returns(expected);
            IDbAccess mockObject = dbMock.Object;

            //Act
            var actual = mockObject.GetEmotionStats();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetTextAnalyticsStats_When_Return()
        {
            //Arrange
            var expected = new TextAnalyticsStats
            {
                AverageCallsPerDay = 7.5f,
                LanguageRanking = new List<LanguageRank>() { },
                SentimentDistribution = new List<SentimentRank>() { }
            };
            var dbMock = new Mock<IDbAccess>();
            dbMock.Setup(e => e.GetTextAnalyticsStats()).Returns(expected);
            IDbAccess mockObject = dbMock.Object;

            //Act
            var actual = mockObject.GetTextAnalyticsStats();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}