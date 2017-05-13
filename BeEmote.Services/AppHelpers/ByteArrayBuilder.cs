///<License terms GNU v3>
/// BeEmote is a simple application that allows you to analyse photos
/// or text with the Microsoft's Cognitive "Emotion API" and "Text Analytics API"
/// Copyright (C) 2017  Romain Vincent
///
/// This program is free software: you can redistribute it and/or modify
/// it under the terms of the GNU General Public License as published by
/// the Free Software Foundation, either version 3 of the License, or
/// (at your option) any later version.
///
/// This program is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
/// GNU General Public License for more details.
///
/// You should have received a copy of the GNU General Public License
/// along with this program.  If not, see <http://www.gnu.org/licenses/>.
/// </License>

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
