using System;
using Xunit;

namespace SamplesToTextsMatcherTests
{
    public class ParserTests
    {
        /// <summary>
        /// =a
        /// </summary>
        [Fact]
        public void EqualSignWoksForSimpleTerm(){
            
        }

        /// <summary>
        /// ="a"
        /// </summary>
        [Fact]
        public void EqualSignWoksForSimpleTermInQuotes()
        {

        }

        /// <summary>
        /// ="a b"
        /// </summary>
        [Fact]
        public void EqualSignWoksForTermsInQuotes()
        {

        }


        /// <summary>
        /// a*
        /// </summary>
        [Fact]
        public void AsterixSignWoksForTerm()
        {

        }
    }
}
