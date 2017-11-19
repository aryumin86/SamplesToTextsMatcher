using System;
namespace SamplesToTextsMatcher.Entities
{
    public class ORExpression : NonTerminalExpression
    {
        public ORExpression(){
            Priority = 2;
            Raw = "|";
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
