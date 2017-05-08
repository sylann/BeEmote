using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeEmote.Core
{
    public interface IDescribable
    {
        /// <summary>
        /// This should print in the console, an exhaustive
        /// but easy to read description of its possessor.
        /// </summary>
        void Describe();
    }
}
