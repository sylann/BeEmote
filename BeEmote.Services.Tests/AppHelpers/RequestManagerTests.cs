using BeEmote.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeEmote.Services.Tests
{
    [TestClass()]
    public class RequestManagerTests
    {
        // Tested class instance
        private readonly RequestManager req = new RequestManager();

        #region IConfigurable tests

        [TestMethod()]
        public void GetEmotionConfiguration_WhenValidLocalImagePath_ReturnValidConfig()
        {
            // Arrange
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "oberyn-wear-helmet.jpg");
            var expected = new RequestConfiguration(
                Uri: "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                Data: ByteArrayBuilder.FromImagePath(imagePath),
                ContentType: "application/octet-stream",
                CredentialKey: Credentials.EmotionKey);

            // Act
            var actual = req.GetEmotionConfiguration(imagePath);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(RequestConfiguration));
            Assert.AreEqual(expected.ContentType, actual.ContentType);
            Assert.AreEqual(expected.CredentialKey, actual.CredentialKey);
            Assert.IsTrue(expected.Data.SequenceEqual(actual.Data));
            Assert.AreEqual(expected.Uri, actual.Uri);
        }

        [TestMethod()]
        public void GetEmotionConfiguration_WhenValidRemoteImagePath_ReturnValidConfig()
        {
            // Arrange
            string imagePath = "http://media.indiatimes.in/media/content/2013/Oct/81964930_1346039027_1382772752_1382772760_540x540.jpg";
            var json = new JsonManager();
            var body = json.GetEmotionJson(imagePath);

            var expected = new RequestConfiguration(
                Uri: "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                Data: ByteArrayBuilder.FromJsonObject(body),
                ContentType: "application/json",
                CredentialKey: Credentials.EmotionKey);

            // Act
            var actual = req.GetEmotionConfiguration(imagePath);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(RequestConfiguration));
            Assert.AreEqual(expected.ContentType, actual.ContentType);
            Assert.AreEqual(expected.CredentialKey, actual.CredentialKey);
            Assert.IsTrue(expected.Data.SequenceEqual(actual.Data));
            Assert.AreEqual(expected.Uri, actual.Uri);
        }

        [TestMethod()]
        public void GetEmotionConfiguration_WhenInvalidLocalImagePath_ReturnNull()
        {
            // Arrange
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "invalid-image-path.jpg");

            // Act
            var actual = req.GetEmotionConfiguration(imagePath);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetEmotionConfiguration_WhenInvalidRemoteImagePath_ReturnValidConfig()
        {
            // We don't handle validity of remote image url because the API does it for us.
            // Thus the method should return a valid configuration.

            // Arrange
            string imagePath = "https://invalid-remote-url-image.com";
            var json = new JsonManager();
            var body = json.GetEmotionJson(imagePath);

            var expected = new RequestConfiguration(
                Uri: "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize",
                Data: ByteArrayBuilder.FromJsonObject(body),
                ContentType: "application/json",
                CredentialKey: Credentials.EmotionKey);

            // Act
            var actual = req.GetEmotionConfiguration(imagePath);

            // Assert
            Assert.IsInstanceOfType(actual, typeof(RequestConfiguration));
            Assert.AreEqual(expected.ContentType, actual.ContentType);
            Assert.AreEqual(expected.CredentialKey, actual.CredentialKey);
            Assert.IsTrue(expected.Data.SequenceEqual(actual.Data));
            Assert.AreEqual(expected.Uri, actual.Uri);
        }

        [TestMethod()]
        public void GetTextAnalyticsConfiguration_WhenValidParameters_ReturnValidConfiguration()
        {
            // Arrange
            var json = new JsonManager();
            var validQueries = new List<string> { "languages", "keyPhrases", "sentiment" };
            var text = "I'm sorry that you had to write all these tests, I really do. But it's for humanity's sake.";
            var language = "English";
            
            foreach (var query in validQueries)
            {
                    var body = query == "languages"
                        ? json.GetTextAnalyticsJson(text)
                        : json.GetTextAnalyticsJson(text, language);

                var expected = new RequestConfiguration(
                    Uri: $"https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/{query}",
                    Data: ByteArrayBuilder.FromJsonObject(body),
                    ContentType: "application/json",
                    CredentialKey: Credentials.TextAnalyticsKey);

                // Act
                var actual = req.GetTextAnalyticsConfiguration(query, text, language);

                // Assert
                Assert.IsInstanceOfType(actual, typeof(RequestConfiguration));
                Assert.AreEqual(expected.ContentType, actual.ContentType);
                Assert.AreEqual(expected.CredentialKey, actual.CredentialKey);
                Assert.IsTrue(expected.Data.SequenceEqual(actual.Data));
                Assert.AreEqual(expected.Uri, actual.Uri);
            }
        }

        [TestMethod()]
        public void GetTextAnalyticsConfiguration_WhenNoLanguage_ReturnNull()
        {
            // In case we want to ask the sentiment or the key phrases
            // of a text from the API, we need to provide a language.

            // arrange
            var language = "";
            var text = "bla bla bla";

            // act
            var actualSentiment = req.GetTextAnalyticsConfiguration("sentiment", text, language);
            var actualKeyPhrases = req.GetTextAnalyticsConfiguration("keyPhrases", text, language);

            // assert
            Assert.IsNull(actualSentiment);
            Assert.IsNull(actualKeyPhrases);
        }

        [TestMethod()]
        public void GetTextAnalyticsConfiguration_WhenInvalidQuery_ThrowArgumentException()
        {
            // arrange
            var language = "";
            var text = "bla bla bla";
            var query = "invalidQuery";

            try
            {
                // act
                var actual = req.GetTextAnalyticsConfiguration(query, text, language);
                // assert
                Assert.Fail("GetTextAnalyticsConfiguration should have thrown an exception");
            }
            catch (ArgumentException) { } // valid exception will be caught
            // Any other exception will make this test fail

        }

        #endregion

        #region IRequest tests

        [TestMethod()]
        public void MakeRequest_When_Return()
        {
            // Chose not to test request for now: 2017-05-27 Romain
            var AnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything = 42;
            Assert.IsTrue(AnswerToTheUltimateQuestionOfLifeTheUniverseAndEverything == 42);
        }

#endregion
    }
}