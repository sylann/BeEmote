using BeEmote.Core;
using BeEmote.Services;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Services.Tests
{
    [TestClass]
    public class EmotionAppTests
    {
        // Tested class instance
        private readonly EmotionManager emotionManager = new EmotionManager();

#region ICognitiveApp implementation

        [TestMethod()]
        public void Start_When_Return()
        {
            // TODO: Test the global Emotion Start Method (permanent x1.5 intelligence)
            
            // Arrange
            // no need to prepare anything

            // Act
            // await textManager.Start(); // async!

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void Reset_When_Return()
        {
            // Arrange
            emotionManager.State = RequestStates.ResponseReceived;
            var expected = RequestStates.NoData;

            // Act
            var actual = emotionManager.Reset();

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected, emotionManager.State);
        }

        [TestMethod()]
        public void SendRequest_When_Return()
        {
            // TODO: Figure out how to test request response for emotion (+500 reputation)
            
            // Arrange

            // Act
            // emotionManager.SendRequest();

            // Assert
            Assert.Fail();
        }

        [TestMethod()]
        public void HandleResult_When_Return()
        {
            // TODO: Test the Emotion HandleResult Method (+100 reputation)

            // Arrange

            // Act
            // emotionManager.HandleResult();

            // Assert
            Assert.Fail();
        }

#endregion

#region IEmotionAPI implementation

        [TestMethod()]
        public void Configure_When_Return()
        {
            // Arrange

            // Act
            // emotionManager.Configure();

            // Assert
            Assert.Fail();
        }

#endregion
    }
}
