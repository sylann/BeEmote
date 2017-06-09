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
        public void UpdateEmotion_When_Return()
        {
            // TODO: Test the database properly
            // Arrange
            var imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invalid-image-path.jpg");
            var faces = new List<Face>();

            // Act
            var actual = db.UpdateEmotion(faces, imagePath);

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTextAnalytics_CorrectValues()
        {
            //Arrange 
            Language language = new Language()
            {
                Name = "Anglais",
                Iso6391Name = "en",
                Score = 100
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
            Mock<DataAccess> mockHelper = new Mock<DataAccess>();

            mockHelper.Setup(e => e.InsertImgAnalysis(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(1);

            //Act
            DataAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreNotEqual(0, mockObject.InsertImgAnalysis(facesCount, imagePath));
        }

        [TestMethod()]
        public void InsertImgAnalysis_Invalid_IdImage()
        {
            //Arrange 
            int facesCount = 4;
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "oberyn-wear-helmet.jpg");
            Mock<DataAccess> mockHelper = new Mock<DataAccess>();

            mockHelper.Setup(e => e.InsertImgAnalysis(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(0);

            //Act
            DataAccess mockObject = mockHelper.Object;

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
            Mock<DataAccess> mockHelper = new Mock<DataAccess>();

            mockHelper.Setup(e => e.InsertEmotion(It.IsAny<List<Face>>(), It.IsAny<int>()))
                .Returns(idImg);

            //Act
            DataAccess mockObject = mockHelper.Object;

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
            Mock<DataAccess> mockHelper = new Mock<DataAccess>();

            mockHelper.Setup(e => e.InsertEmotion(It.IsAny<List<Face>>(), It.IsAny<int>()))
                .Returns(idImg);

            //Act
            DataAccess mockObject = mockHelper.Object;

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

            Mock<DataAccess> mockHelper = new Mock<DataAccess>();

            //Act

            mockHelper.Setup(e => e.InsertTextAnalysis(It.IsAny<Language>(), It.IsAny<double?>(), It.IsAny<string>()))
                .Returns(idText);
            DataAccess mockObject = mockHelper.Object;

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

            Mock<DataAccess> mockHelper = new Mock<DataAccess>();

            //Act

            mockHelper.Setup(e => e.InsertTextAnalysis(It.IsAny<Language>(), It.IsAny<double?>(), It.IsAny<string>()))
                .Returns(idText);
            DataAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreEqual(0, mockObject.InsertTextAnalysis(lang, sentiment, textContent));
        }

        [TestMethod]
        public void GetImgAverageCallsPerDay_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetAverageFaceCount_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetDominantRanking_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetTextAverageCallsPerDay_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetLanguageRanking_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void GetSentimentDistribution_When_Return()
        {
            Assert.Fail();
        }
    }
}