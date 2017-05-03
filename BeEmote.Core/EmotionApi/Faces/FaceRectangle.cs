namespace BeEmote.Core
{
    /// <summary>
    /// This class corresponds to a JSON structure as defined by the Microsoft's Emotion API.
    /// It provides position and size of a rectangle containing a face.
    /// The JSON.net Deserializer automatically sets the public properties.
    /// </summary>
    public class FaceRectangle
    {
        /// <summary>
        /// The distance between the left of the image and the left of the rectangle containing the face
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// The distance between the top of the image and the top of the rectangle containing the face
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// The width of the rectangle containing the face
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// The height of the rectangle containing the face
        /// </summary>
        public int Height { get; set; }
    }
}
