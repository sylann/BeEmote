using BeEmote.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BeEmote.Core.Tests
{
    [TestClass]
    public class EmotionTests
    {
        [TestMethod()]
        public void GetDominantEmotion_ReturnCorrectDominant()
        {
            // Arrange
            var angerFace = new Face() { Scores = new Scores { Anger = 1, Contempt = 0, Disgust = 0, Fear = 0, Happiness = 0, Neutral = 0, Sadness = 0, Surprise = 0 } };
            var contemptFace = new Face() { Scores = new Scores { Anger = 0, Contempt = 1, Disgust = 0, Fear = 0, Happiness = 0, Neutral = 0, Sadness = 0, Surprise = 0 } };
            var disgustFace = new Face() { Scores = new Scores { Anger = 0, Contempt = 0, Disgust = 1, Fear = 0, Happiness = 0, Neutral = 0, Sadness = 0, Surprise = 0 } };
            var fearFace = new Face() { Scores = new Scores { Anger = 0, Contempt = 0, Disgust = 0, Fear = 1, Happiness = 0, Neutral = 0, Sadness = 0, Surprise = 0 } };
            var happinessFace = new Face() { Scores = new Scores { Anger = 0, Contempt = 0, Disgust = 0, Fear = 0, Happiness = 1, Neutral = 0, Sadness = 0, Surprise = 0 } };
            var neutralFace = new Face() { Scores = new Scores { Anger = 0, Contempt = 0, Disgust = 0, Fear = 0, Happiness = 0, Neutral = 1, Sadness = 0, Surprise = 0 } };
            var sadnessFace = new Face() { Scores = new Scores { Anger = 0, Contempt = 0, Disgust = 0, Fear = 0, Happiness = 0, Neutral = 0, Sadness = 1, Surprise = 0 } };
            var surpriseFace = new Face() { Scores = new Scores { Anger = 0, Contempt = 0, Disgust = 0, Fear = 0, Happiness = 0, Neutral = 0, Sadness = 0, Surprise = 1 } };

            // Act
            var angerFaceDominant = angerFace.GetDominantEmotion();
            var contemptFaceDominant = contemptFace.GetDominantEmotion();
            var disgustFaceDominant = disgustFace.GetDominantEmotion();
            var fearFaceDominant = fearFace.GetDominantEmotion();
            var happinessFaceDominant = happinessFace.GetDominantEmotion();
            var neutralFaceDominant = neutralFace.GetDominantEmotion();
            var sadnessFaceDominant = sadnessFace.GetDominantEmotion();
            var surpriseFaceDominant = surpriseFace.GetDominantEmotion();

            // Assert
            Assert.AreEqual(Emotions.Anger, angerFaceDominant);
            Assert.AreEqual(Emotions.Contempt, contemptFaceDominant);
            Assert.AreEqual(Emotions.Disgust, disgustFaceDominant);
            Assert.AreEqual(Emotions.Fear, fearFaceDominant);
            Assert.AreEqual(Emotions.Happiness, happinessFaceDominant);
            Assert.AreEqual(Emotions.Neutral, neutralFaceDominant);
            Assert.AreEqual(Emotions.Sadness, sadnessFaceDominant);
            Assert.AreEqual(Emotions.Surprise, surpriseFaceDominant);
        }

        [TestMethod()]
        public void ToString_Returns_Correct_Format()
        {
            // Arrange
            var lang = new Language() { Name = "English", Iso6391Name = "en", Score = 0.5 };
            string expected = "English[en] (50%)";

            // Act
            string result = lang.ToString();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }

}
