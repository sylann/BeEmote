using System;
using System.Collections.Generic;
using System.Linq;

namespace BeEmote.Core
{
    public class Face
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
            Console.WriteLine($"  Dominant Emotion: {GetDominantEmotion()}");
            Console.WriteLine($"  Height: { FaceRectangle.Height }");
            Console.WriteLine($"  Left:   { FaceRectangle.Left   }");
            Console.WriteLine($"  Top:    { FaceRectangle.Top    }");
            Console.WriteLine($"  Width:  { FaceRectangle.Width  }");
            Console.WriteLine($"  Anger:     { Scores.AngerHR     }");
            Console.WriteLine($"  Contempt:  { Scores.ContemptHR  }");
            Console.WriteLine($"  Disgust:   { Scores.DisgustHR   }");
            Console.WriteLine($"  Fear:      { Scores.FearHR      }");
            Console.WriteLine($"  Happiness: { Scores.HappinessHR }");
            Console.WriteLine($"  Neutral:   { Scores.NeutralHR   }");
            Console.WriteLine($"  Sadness:   { Scores.SadnessHR   }");
            Console.WriteLine($"  Surprise:  { Scores.SurpriseHR  }\n");
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
