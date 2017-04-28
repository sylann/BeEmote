using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Services.Tests
{
    [TestClass]
    public class RequestTests
    {
        #region Emotion Tests

        [TestMethod]
        public void Emotion_WhenValidPath_ReturnString()
        {
            // Tested Method: MakeEmotionRequest(string ImagePath)
            // Providing a valid image path, the method should return a string
            Assert.Fail();
        }

        [TestMethod]
        public void Emotion_WhenValidPath_ResponseIsValid()
        {
            // Tested Method: MakeEmotionRequest(string ImagePath)
            // Providing a valid image path, the response structure should be valid
            Assert.Fail();
        }

        [TestMethod]
        public void Emotion_WhenInvalidPath_ReturnNull()
        {
            // Tested Method: MakeEmotionRequest(string ImagePath)
            // Providing an invalid image path, the method should return null
            Assert.Fail();
        }

        [TestMethod]
        public void Emotion_WhenUnexpectedResponse_ReturnNull()
        {
            // Tested Method: MakeEmotionRequest(string ImagePath)
            // Receiving an unexpected response structure, the method should return null
            Assert.Fail();
        }

        #endregion

        #region Text Analytics Tests

        [TestMethod]
        public void TextAnalytics_WhenValidRequest_ReturnString()
        {
            // Tested Method: MakeTextAnalyticsRequest(string Query, string JsonBody)
            // Providing a valid request, the method should return a string
            Assert.Fail();
        }

        [TestMethod]
        public void TextAnalytics_WhenValidRequest_ResponseIsValid()
        {
            // Tested Method: MakeTextAnalyticsRequest(string Query, string JsonBody)
            // Providing a valid request, the response json structure should be valid
            Assert.Fail();
        }

        [TestMethod]
        public void TextAnalytics_WhenInvalidRequest_ReturnNull()
        {
            // Tested Method: MakeTextAnalyticsRequest(string Query, string JsonBody)
            // Providing an invalid request, the method should return null
            Assert.Fail();
        }

        [TestMethod]
        public void TextAnalytics_WhenUnexpectedResponse_ReturnNull()
        {
            // Tested Method: MakeTextAnalyticsRequest(string Query, string JsonBody)
            // Receiving an unexpected response structure, the method should return null
            Assert.Fail();
        }

        #endregion
    }
}
