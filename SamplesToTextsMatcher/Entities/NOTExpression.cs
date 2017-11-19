using System;
namespace SamplesToTextsMatcher.Entities
{
    public class NOTExpression : NonTerminalExpression
    {
        public NOTExpression(){
            Priority = 3;
            Raw = "~";
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
