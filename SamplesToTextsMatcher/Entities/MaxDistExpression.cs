using System;
namespace SamplesToTextsMatcher.Entities
{
    public class MaxDistExpression : NonTerminalExpression
    {
        /// <summary>
        /// N between term 1 and term 2 (in A /n B expression).
        /// </summary>
        /// <value>The n.</value>
        public int N { get; set; }

        public MaxDistExpression(int n){
            Priority = 3;
            Raw = "/" + n;
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
