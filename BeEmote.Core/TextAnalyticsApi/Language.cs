using BeEmote.Common;
using System;

namespace BeEmote.Core
{
    /// <summary>
    /// Informations on the language that can be detected in a text by the Microsoft's Text Analytics API.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Name of the language.
        ///   Ex: 'English'
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This is the 2 character norm notation corresponding to the language Name.
        ///   Ex: 'en'
        /// </summary>
        public string Iso6391Name { get; set; }

        /// <summary>
        /// The score represents the confidence in the language detection.
        ///   Ex: 0.56
        /// </summary>
        public double Score { get; set; }

        /// <summary>
        /// Describes the class properties
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Name}[{Iso6391Name}] ({Formatter.Percent(Score)})";
        }
    }
}
