using SamplesToTextsMatcher;
using SamplesToTextsMatcher.Entities;
using System;
using Xunit;

namespace SamplesToTextsMatcherTests
{
    public class ParserTests
    {
        AbstractPatternParser parser = new ConcretePatternParser();

        /// <summary>
        /// =a
        /// </summary>
        [Fact]
        public void EqualSignWoksForSimpleTerm(){
            string pattern = "=корелла";
            Context context = new Context(pattern, parser, null);
            Assert.True(((TerminalExpression)context.Root).NeedsExactForm);
        }

        /// <summary>
        /// ="a"
        /// </summary>
        [Fact]
        public void EqualSignWoksForSimpleTermInQuotes()
        {
            string pattern = "=\"корелла\"";
            Context context = new Context(pattern, parser, null);
            Assert.True(((TerminalExpression)context.Root).NeedsExactForm);
        }

        /// <summary>
        /// ="a b"
        /// </summary>
        [Fact]
        public void EqualSignWoksForSeveralTermsInQuotes()
        {
            string pattern = "=\"синяя корелла\"";
            Context context = new Context(pattern, parser, null);
            Assert.True(((TerminalExpression)context.Root).NeedsExactForm);
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
