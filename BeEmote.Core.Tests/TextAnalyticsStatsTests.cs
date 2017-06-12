using Microsoft.VisualStudio.TestTools.UnitTesting;
using BeEmote.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeEmote.Core.Tests
{
    [TestClass()]
    public class TextAnalyticsStatsTests
    {
        [TestMethod()]
        public void TextAnalyticsStats_ToString_Returns()
        {
            // Arrange
            var ttt = new TextAnalyticsStats()
            {
                AverageCallsPerDay = 5.5f,
                LanguageRanking = new List<LanguageRank>() { new LanguageRank() { Name = "English", Proportion = 0.95 } },
                SentimentDistribution = new List<SentimentRank>() { new SentimentRank() { Rank = "[0.00 - 0.30]", Count = 10 } }
            };
            var expected = "Average calls per day: 5,5\nLanguageRanking:\n- English: 0,95\nSentimentDistribution:\n- [0.00 - 0.30]: 10\n";
            // Act
            var actual = ttt.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void LanguageRank_ToString_Returns()
        {
            // Arrange
            var ttt = new LanguageRank()
            {
                Name = "English",
                Proportion = 0.55
            };
            var expected = "English: 0,55";
            // Act
            var actual = ttt.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SentimentRank_ToString_Returns2()
        {
            // Arrange
            var ttt = new SentimentRank()
            {
                Rank = "[0.00 - 0.30]",
                Count = 10
            };
            var expected = "[0.00 - 0.30]: 10";
            // Act
            var actual = ttt.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}