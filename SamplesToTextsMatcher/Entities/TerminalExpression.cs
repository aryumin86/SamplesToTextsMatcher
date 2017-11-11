using System;
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

        public TerminalExpression(string symbol){
            Raw = symbol;
            NeedsExactForm = true;
            HasAsterixSign = false;
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
