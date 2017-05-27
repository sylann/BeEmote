using BeEmote.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

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
        public void UpdateTextAnalytics_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertImgAnalysis_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertEmotion_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void InsertTextAnalysis_When_Return()
        {
            Assert.Fail();
        }
    }
}