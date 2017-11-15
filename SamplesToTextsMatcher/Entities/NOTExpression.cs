using System;
namespace SamplesToTextsMatcher.Entities
{
    public class NOTExpression : NonTerminalExpression
    {
        public NOTExpression(){
            Priority = 2;
            Raw = "~";
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
