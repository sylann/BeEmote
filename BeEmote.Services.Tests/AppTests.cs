using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Services.Tests
{
    [TestClass]
    public class AppTests
    {
        #region StartEmotion Tests

        [TestMethod]
        public void Emotion_AfterStart_RequestShouldBeSet()
        {
            // Tested Method: StartEmotion()
            // After having executed the method, the request property should be initialized
            Assert.Fail();
        }

        #endregion

        #region StartTextAnalytics Tests

        // TODO: ADD tests that verify the availability of the TextAnalyticsApiResponse class and its properties after StartTextAnalytics()

        #endregion
    }
}
