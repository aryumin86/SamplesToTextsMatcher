using System;
namespace SamplesToTextsMatcher.Entities
{
    public class ORExpression : NonTerminalExpression
    {
        public ORExpression(){
            Priority = 1;
            Raw = "|";
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
