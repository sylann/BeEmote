using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Text;

namespace BeEmote.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ByteArrayBuilder
    {
        #region Public Methods

        /// <summary>
        /// Transform an image from its local path into an array of byte
        /// </summary>
        /// <param name="imageFilePath">The full path to the image</param>
        /// <returns></returns>
        public static byte[] FromImagePath(string imageFilePath)
        {
            try
            {
                FileStream fileStream = new FileStream(imageFilePath, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to read the given file: {imageFilePath}\nError: {e}");
                return null;
            }
        }

        /// <summary>
        /// Transforms a string representing a json body into an array of byte
        /// </summary>
        /// <param name="json">The string representing the json body</param>
        /// <returns></returns>
        public static byte[] FromJsonObject(JObject json)
        {
            return Encoding.UTF8.GetBytes(json.ToString());
        }

        #endregion
    }
}
