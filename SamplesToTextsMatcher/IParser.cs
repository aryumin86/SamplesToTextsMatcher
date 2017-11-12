using System;
namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Parser of pattern.
    /// </summary>
    public abstract class IParser
    {
        /// <summary>
        /// Raw input string after text basic formating.
        /// </summary>
        protected string input;

        /// <summary>
        /// Validates the input pattern for brackets and quotes signs, 
        /// stars and other non terminal usage accoring to grammar.
        /// </summary>
        /// <returns><c>true</c>, if input was validated, <c>false</c> otherwise.</returns>
        /// <param name="input">Input.</param>
        public abstract bool ValidateInput(string input);

        public abstract void ParseInput();
    }
}
