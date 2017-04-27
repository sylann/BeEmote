using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeEmote.Core
{
    /// <summary>
    /// Information resulting from sending an image request to the Microsoft's Emotion API.
    /// It concists of a list of <see cref="Face"/>s describing the position
    /// and the <see cref="Emotion"/> of each face found on the image.
    /// </summary>
    public class EmotionApiResponse
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
            System.Console.WriteLine($"{NbFaces} trouvé(s)\n");

            foreach (Face face in Faces)
                face.Describe();
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor.
        /// Deserialize the json response into this composite class.
        /// </summary>
        public EmotionApiResponse(string json)
        {
            // Deserialize the json (instanciate classes)
            Faces = JsonConvert.DeserializeObject<List<Face>>(json);
        }

        #endregion
    }
}
