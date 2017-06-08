﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeEmote.Core
{
    /// <summary>
    /// Associates a sentiment rank to its count of occurrences in the database.
    /// </summary>
    public class SentimentRank
    {
        /// <summary>
        /// The sentiment Rank:
        ///   "0.00 - 0.30"
        ///   "0.31 - 0.60"
        ///   "0.61 - 1.00"
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// The count of occurrences in the database.
        /// </summary>
        public int Count { get; set; }
    }
}
