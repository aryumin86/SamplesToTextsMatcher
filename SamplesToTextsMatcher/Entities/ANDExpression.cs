using System;
namespace SamplesToTextsMatcher.Entities
{
    public class ANDExpression : NonTerminalExpression
    {
        public ANDExpression(){
            Priority = 3;
            Raw = "&";
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
