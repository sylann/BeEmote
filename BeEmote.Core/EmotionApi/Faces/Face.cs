using BeEmote.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeEmote.Core
{
    public class Face : IDescribable
    {
        #region Public Properties

        /// <summary>
        /// The rectangle returned by the Microsoft's Emotion API
        /// </summary>
        public FaceRectangle FaceRectangle { get; set; }

        /// <summary>
        /// The scores returned by the Microsoft's Emotion API
        /// </summary>
        public Scores Scores { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Finds the name of the Emotion with the highest score
        /// </summary>
        /// <returns>A name that corresponds to one of the values in <see cref="Emotions"/></returns>
        public Emotions GetDominantEmotion()
        {
            // Produce the list of emotions
            List<Emotion> EmotionList = BuildEmotionList();

            // Find the Emotion with the highest score
            Emotion dominant = EmotionList.Aggregate((currentDominant, emotion) =>
                currentDominant == null || emotion.Score > currentDominant.Score
                    ? emotion // emotion is bigger or first value
                    : currentDominant); // current is bigger

            // Returns the corresponding name
            return dominant.Name;
        }

        /// <summary>
        /// Provides a verbose description of the face:
        /// Dominant emotion, rectangle position and size and each emotion scores.
        /// </summary>
        public void Describe()
        {
            Console.WriteLine("Dominant Emotion:  {0},    Face Position:  {1}(Left), {2}(Top), {3}(Width), {4}(Height)"
                , GetDominantEmotion()
                , FaceRectangle.Left
                , FaceRectangle.Top
                , FaceRectangle.Width
                , FaceRectangle.Height);
            Console.WriteLine("| Surprise:   {0,6} | Contempt:  {1,6} | Disgust:  {2,6} | Fear:   {3,6} |"
                , Formatter.Percent(Scores.Surprise)
                , Formatter.Percent(Scores.Contempt)
                , Formatter.Percent(Scores.Disgust)
                , Formatter.Percent(Scores.Fear));
            Console.WriteLine("| Happiness:  {0,6} | Neutral:   {1,6} | Sadness:  {2,6} | Anger:  {3,6} |\n"
                , Formatter.Percent(Scores.Happiness)
                , Formatter.Percent(Scores.Neutral)
                , Formatter.Percent(Scores.Sadness)
                , Formatter.Percent(Scores.Anger));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Makes a list of the valid <see cref="Emotions"/> with the current scores of this face.
        /// Each <see cref="Emotion"/> have a name and a score.
        /// </summary>
        /// <returns></returns>
        private List<Emotion> BuildEmotionList()
        {
            return new List<Emotion>
            {
                new Emotion(Emotions.Anger, Scores.Anger),
                new Emotion(Emotions.Contempt, Scores.Contempt),
                new Emotion(Emotions.Disgust, Scores.Disgust),
                new Emotion(Emotions.Fear, Scores.Fear),
                new Emotion(Emotions.Happiness, Scores.Happiness),
                new Emotion(Emotions.Neutral, Scores.Neutral),
                new Emotion(Emotions.Sadness, Scores.Sadness),
                new Emotion(Emotions.Surprise, Scores.Surprise)
            };
        }

        #endregion
    }
}
