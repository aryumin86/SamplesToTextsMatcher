using System;
using System.Linq;

namespace SamplesToTextsMatcher.Entities
{
    public class TerminalExpression : Expression
    {
        /// <summary>
        /// This term shold be used in this exact form (no term forms are needed).
        /// </summary>
        /// <value><c>true</c> if needs exact form; otherwise, <c>false</c>.</value>
        public bool NeedsExactForm { get; set; }

        /// <summary>
        /// Term has asterix sign at the end.
        /// </summary>
        /// <value><c>true</c> if has asterix sign; otherwise, <c>false</c>.</value>
        public bool HasAsterixSign { get; set; }

        /// <summary>
        /// This term is included in quotes.
        /// </summary>
        /// <value><c>true</c> if in quotes; otherwise, <c>false</c>.</value>
        public bool InQuotes { get; set; }

        public TerminalExpression(string term)
        {
            Raw = term;
        }

        public override bool Interpret(Context context)
        {
            if (string.IsNullOrEmpty(Raw))
                throw new FormatException("No value for terminal");

            if (context.CurrentStringToMatchWithTree.Contains(Raw))
                return true;

            return false;
        }
    }
}
