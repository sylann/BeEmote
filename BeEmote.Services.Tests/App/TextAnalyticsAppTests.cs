using BeEmote.Core;
using BeEmote.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Services.Tests
{
    [TestClass]
    public class TextAnalyticsAppTests
    {
        // Tested class instance
        private readonly TextAnalyticsManager textManager = new TextAnalyticsManager();

#region ICognitiveApp Tests

        [TestMethod()]
        public void Start_When_Return()
        {
            // TODO: Test the global TextAnalytics Start Method (permanent x1.5 intelligence)

            // Arrange
            // no need to prepare anything

            // Act
            // await textManager.Start(); // async!

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void Reset_WhenReset_StateShouldBeDefaultValue()
        {
            // Arrange
            textManager.State = RequestStates.ResponseReceived;
            var expected = RequestStates.NoData;

            // Act
            var actual = textManager.Reset();

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, textManager.State);
        }

        [TestMethod()]
        public void SendRequest_When_Return()
        {
            // TODO: Figure out how to test request response for text analytics (+500 reputation)

            // Arrange
            var uri = "";
            var data = new byte[32];
            var type = "application/octet-stream";
            var credential = Credentials.EmotionKey;
            var config = new RequestConfiguration(uri, data, type, credential);

            // Act
            //string actualJson = await textManager.SendRequest(config);

            // Assert
            Assert.Fail();
        }

#endregion

#region ITextAnalyticsAPI Tests

        [TestMethod()]
        public void Configure_When_Return()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateLanguage_When_Return()
        {
            // TODO: Test the TextAnalytics UpdateLanguage Method (+100 reputation)

            // Arrange
            var jsonResponse = "";

            // Act
            textManager.UpdateLanguage(jsonResponse);

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateKeyPhrases_When_Return()
        {
            // TODO: Test the TextAnalytics UpdateKeyPhrases Method (+100 reputation)

            // Arrange
            var jsonResponse = "";

            // Act
            textManager.UpdateKeyPhrases(jsonResponse);

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateSentiment_When_Return()
        {
            // TODO: Test the TextAnalytics UpdateSentiment Method (+100 reputation)

            // Arrange
            var jsonResponse = "";

            // Act
            textManager.UpdateSentiment(jsonResponse);

            // Assert
            Assert.Fail();
        }

#endregion
    }
}
