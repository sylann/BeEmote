namespace BeEmote.Core
{
    /// <summary>
    /// Associates a language name to its count of occurrences in the database
    /// </summary>
    public class LanguageRank
    {
        /// <summary>
        /// The language name, for example:
        ///   English, French, ...
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The proportion of presence of the language in the database:
        ///   from 0.00 to 1.00
        /// </summary>
        public double? Proportion { get; set; }

        public override string ToString()
        {
            return $"{Name}: {Proportion}";
        }
    }
}
