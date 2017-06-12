using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BeEmote.Core.Tests
{
    [TestClass()]
    public class EmotionStatsTests
    {
        [TestMethod()]
        public void ToString_Returns()
        {
            // Arrange
            var ttt = new EmotionStats()
            {
                AverageCallsPerDay = 5.5f,
                AverageFaceCount = 3.5f,
                DominantRanking = new List<EmotionRank>() { new EmotionRank() { Name = "Anger", Count = 10 } }
            };
            var expected = "Average calls per day: 5,5\nAverage face count: 3,5\nDominantRanking:\n- Anger: 10\n";
            // Act
            var actual = ttt.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void EmotionRank_ToString_Returns()
        {
            // Arrange
            var ttt = new EmotionRank()
            {
                Name = "Happiness",
                Count = 10
            };
            var expected = "Happiness: 10";
            // Act
            var actual = ttt.ToString();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}