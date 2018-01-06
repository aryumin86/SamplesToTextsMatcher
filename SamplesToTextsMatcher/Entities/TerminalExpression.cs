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

        /// <summary>
        /// This term should be replaced with another linked list of expressions.
        /// If it should be replaced it has to have format like: {the_term}.
        /// </summary>
        /// <value><c>true</c> if should be replaced; otherwise, <c>false</c>.</value>
        public bool ShouldBeReplaced { get; set; }

        public TerminalExpression(string term, bool shouldBeReplaced = false)
        {
            Raw = term;
            ShouldBeReplaced = shouldBeReplaced;
        }

        public override bool Interpret(Context context)
        {
            if (string.IsNullOrEmpty(Raw))
                throw new FormatException("No value for terminal");

            TermRepresentedInRaw = new bool[context.CurrentStringToMatchWithTree.Length];

            ResStringExpression = Raw;

            bool res = false;

            for (int i = 0; i < context.CurrentStringToMatchWithTree.Length; i++){
                if(context.CurrentStringToMatchWithTree[i] == Raw){
                    TermRepresentedInRaw[i] = true;
                    res = true;
                }
            }

            return res;
        }
    }
}
