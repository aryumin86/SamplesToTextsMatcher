using System;
namespace SamplesToTextsMatcher.Entities
{
    public class TerminalExpression : Expression
    {
        private string _symbol;

        public TerminalExpression(string symbol){
            _symbol = symbol;
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
