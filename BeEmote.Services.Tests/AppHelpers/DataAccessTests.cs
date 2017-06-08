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
            Language language = new Language() {Name = "Anglais",
                                                Iso6391Name = "en",
                                                Score = 100};
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

            List<Face> faces;
            int idImg = 1;
            Mock<DataAccess> mockHelper = new Mock<DataAccess>();

            mockHelper.Setup(e => e.InsertEmotion(It.IsAny<List<Face>>(), It.IsAny<int>()))
                .Returns(0);

            //Act
            DataAccess mockObject = mockHelper.Object;

            //Assert
            Assert.AreEqual(0, mockObject.InsertImgAnalysis(facesCount, imagePath));
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTextAnalysis_When_Return()
        {
            Assert.Fail();
        }
    }
}