using System;
using SamplesToTextsMatcher;
using Xunit;

namespace SamplesToTextsMatcherTests
{
    public class MatcherTests
    {
        /// <summary>
        /// a
        /// </summary>
        [Fact]
        public void SingleWordPatternMatchesCorrectly(){
            string pattern = "корелла";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                    "корелла"
            });

            Assert.True(res);
        }

        /// <summary>
        /// a & b
        /// </summary>
        [Fact]
        public void SimpleANDExpressionPatternMatchesCorrectly()
        {
            string pattern = "корелла & арбуз";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                "корелла",
                "бегемот",
                "арбуз"
            });

            Assert.True(res);
        }

        /// <summary>
        /// a | b
        /// </summary>
        [Fact]
        public void SimpleORExpressionPatternMatchesCorrectly()
        {
            string pattern = "корелла | арбуз";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                "корелла",
                "бегемот"
            });

            Assert.True(res);

            res = context.MatchPatternToString(new string[]{
                "арбуз",
                "птичка"
            });

            Assert.True(res);
        }

        /// <summary>
        /// a ~b
        /// </summary>
        [Fact]
        public void SimpleNOTExpressionPatternMatchesCorrectly()
        {
            string pattern = "корелла ~арбуз";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                "корелла",
                "арбуз",
                "какаду"
            });

            Assert.False(res);
        }

        /// <summary>
        /// a /n b
        /// </summary>
        [Fact]
        public void SimpleMaxDistExpressionPatternMatchesCorrectly()
        {
            string pattern = "корелла /3 арбуз";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                "корелла",
                "выдра",
                "выхухоль",
                "арбуз",
                "какаду"
            });

            Assert.True(res);
        }

        /// <summary>
        /// (a | b) & (c | d)
        /// </summary>
        [Fact]
        public void TwoOrInBracketsAsAndExpressionPatternMatchesCorrectly()
        {
            string pattern = "(корелла | какаду) & (арбуз | ананас)";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                "корелла",
                "выдра",
                "выхухоль",
                "арбуз",
                "чайка"
            });

            Assert.True(res);
        }

        /// <summary>
        /// (a | b) /n (c | d)
        /// </summary>
        [Fact]
        public void TwoOrInBracketsAsMaxDistExpressionPatternMatchesCorrectly()
        {
            string pattern = "(корелла | какаду) /3 (арбуз | ананас)";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                "корелла",
                "выдра",
                "выхухоль",
                "арбуз",
                "чайка"
            });

            Assert.True(res);
        }

        /// <summary>
        /// "a b" /n "c d"
        /// </summary>
        [Fact]
        public void TwoInQuotesTerminalsAsMaxDistExpressionPatternMatchesCorrectly()
        {
            string pattern = "\"синяя корелла\" /3 \"зеленая какаду\"";
            Context context = new Context(pattern, null);
            bool res = context.MatchPatternToString(new string[]{
                "\"синяя корелла\"",
                "выдра",
                "выхухоль",
                "арбуз",
                "\"зеленая какаду\""
            });

            Assert.True(res);
        }
    }
}
