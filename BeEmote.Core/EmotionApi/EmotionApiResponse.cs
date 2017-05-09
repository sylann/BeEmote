using System;
using System.Collections.Generic;

namespace BeEmote.Core
{
    /// <summary>
    /// Information resulting from sending an image request to the Microsoft's Emotion API.
    /// It concists of a list of <see cref="Face"/>s describing the position
    /// and the <see cref="Emotion"/> of each face found on the image.
    /// </summary>
    public class EmotionApiResponse : IDescribable
    {
        #region Public Properties

        /// <summary>
        /// The list of the faces found in the image
        /// </summary>
        public List<Face> Faces { get; }

        /// <summary>
        /// The number of faces indentified on the image
        /// </summary>
        public int NbFaces { get => Faces.Count; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Provides a verbose description of the Emotion API Response.
        /// Makes use of the <see cref="Face.Describe()"/> method.
        /// </summary>
        public void Describe()
        {
            Console.WriteLine($"\n===================\nEmotion API Result:\n");
            if (Faces == null || Faces.Count == 0)
                Console.WriteLine($"Oow. No face were found in your image.\n");
            else
            {
                Console.WriteLine($"{NbFaces} face(s) found!\n");
                foreach (Face face in Faces)
                    face.Describe();
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor.
        /// Deserialize the json response into this composite class.
        /// </summary>
        public EmotionApiResponse(List<Face> Faces)
        {
            this.Faces = Faces;
        }

        #endregion
    }
}
