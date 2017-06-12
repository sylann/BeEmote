using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BeEmote.Core.Tests
{

    [TestClass]
    public class TextAnalyticsTests
    {

        [TestMethod()]
        public void Sentiment_WhenScoreIsNull_ReturnCorrectStrings()
        {
            // Arrange
            var feelingsWithoutValue = new TextAnalyticsApiResponse() { Score = null };
            string expectedWithoutValue = "-";

            // Act
            string resultWithoutValue = feelingsWithoutValue.Sentiment;

            // Assert
            Assert.AreEqual(expectedWithoutValue, resultWithoutValue);
        }

        [TestMethod()]
        public void Sentiment_WhenScoreIsNotNull_ReturnCorrectStrings()
        {
            // Arrange
            var feelingsScoreTest = new TextAnalyticsApiResponse() { Score = 0.5 };
            string expectedTest = "50%";
            var feelingsScoreOne = new TextAnalyticsApiResponse() { Score = 1 };
            string expectedOne = "100%";
            var feelingsScoreZero = new TextAnalyticsApiResponse() { Score = 0 };
            string expectedZero = "0%";

            // Act
            string resultTest = feelingsScoreTest.Sentiment;
            string resultOne = feelingsScoreOne.Sentiment;
            string resultZero = feelingsScoreZero.Sentiment;

            // Assert
            Assert.AreEqual(expectedTest, resultTest);
            Assert.AreEqual(expectedOne, resultOne);
            Assert.AreEqual(expectedZero, resultZero);
        }

        [TestMethod()]
        public void FormattedLanguage_WhenLanguageIsNull_ReturnCorrectStrings()
        {
            // Arrange
            var formatLang = new TextAnalyticsApiResponse() { Language = null };
            string excepted = "Unknown language";

            // Act
            string result = formatLang.FormattedLanguage;

            // Assert
            Assert.AreEqual(excepted, result);
        }

        [TestMethod()]
        public void FormattedLanguage_WhenLanguageIsNotNull_ReturnCorrectStrings()
        {
            // Arrange
            var formatLang = new TextAnalyticsApiResponse()
            {
                Language = new Language()
                {
                    Name = "English",
                    Iso6391Name = "en",
                    Score = 0.5
                }
            };
            string expected = "English[en] (50%)";

            // Act
            string result = formatLang.FormattedLanguage;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void FormattedKeyPhrases_WhenListIsNotEmptyReturnCorrectList()
        {
            // Arrange
            var keyWords = new TextAnalyticsApiResponse()
            {
                KeyPhrases = new List<string> {
                    "chaussure", "poney", "licorne"
                }
            };
            var expected = "chaussure, poney, licorne";

            // Act
            string result = keyWords.FormattedKeyPhrases;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void FormattedKeyPhrases_WhenListIsEmpty_ReturnCorrectList()
        {
            // Arrange
            var keyWords = new TextAnalyticsApiResponse()
            {
                KeyPhrases = new List<string> { "" }
            };
            var expected = "";

            // Act
            string result = keyWords.FormattedKeyPhrases;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void NbKeyPhrases_WhenListIsNull()
        {
            // Arrange
            var nbKey = new TextAnalyticsApiResponse() { KeyPhrases = null };
            int expected = 0;

            // Act
            int result = nbKey.NbKeyPhrases;

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void NbKeyPhrases_WhenListIsNotNull()
        {
            // Arrange
            var nbKey = new TextAnalyticsApiResponse()
            {
                KeyPhrases = new List<string>
                {
                    "chaussure", "poney", "licorne"
                }
            };
            int expected = 3;

            // Act
            int result = nbKey.NbKeyPhrases;

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}
