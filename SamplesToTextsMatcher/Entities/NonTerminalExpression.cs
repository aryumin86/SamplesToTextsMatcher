using System;
namespace SamplesToTextsMatcher.Entities
{
    public abstract class NonTerminalExpression : Expression
    {
        public abstract override void Interpret(Context context);
    }
}
