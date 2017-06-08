using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeEmote.Core
{
    /// <summary>
    /// Associates a language name to its count of occurrences in the database
    /// </summary>
    public class LanguageRank
    {
        /// <summary>
        /// The language name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The proportion of presence of the language in the database.
        /// </summary>
        public int Proportion { get; set; }
    }
}
