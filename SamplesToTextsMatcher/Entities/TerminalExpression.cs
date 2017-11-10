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

        public TerminalExpression(string symbol){
            Raw = symbol;
            NeedsExactForm = true;
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
