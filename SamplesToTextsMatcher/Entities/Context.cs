using System;
namespace SamplesToTextsMatcher.Entities
{
    /// <summary>
    /// Input string as tokens array where index is token's position
    /// (Context of interpret operations for terminal and non terminal expressions).
    /// </summary>
    public class Context
    {
        public string[] Input { get; }

        public Context(string[] input){
            this.Input = input;
        }
    }
}
