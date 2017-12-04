using System;
using System.Collections.Generic;

namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Abstract morfological dictionary.
    /// </summary>
    public abstract class AbstractMorfDictionary
    {
        public abstract List<string> GetSyns(string word, int max = Int32.MaxValue);
    }
}
