using System;
using System.Linq.Expressions;
using SamplesToTextsMatcher.Entities;
using System.Collections.Generic;

namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Parses pattern.
    /// </summary>
    public abstract class AbstractPatternParser
    {
        public abstract LinkedList<Entities.Expression> ParsePattern(string pattern);
    }
}
