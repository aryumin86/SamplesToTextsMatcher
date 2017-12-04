using System;
using SamplesToTextsMatcher;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            //string pattern = "свин | корелла & корова";
            //string pattern = "(свин | корелла) & корова";
            //string pattern = "((свин | корелл*) /2 (=корова | \"оранжевый бобер\")) ~какаду";
            string pattern = "\"мокрая корелла\" & волнистый";

            Context context = new Context(pattern, new ConcretePatternParser(), null);


            bool match7 = context.MatchPatternToString(new string[]{
                "корова", "и", "и", "\"мокрая корелла\"", "и", "волнистый"
            });

            bool match6 = context.MatchPatternToString(new string[]{
                "корова", "и", "и", "корелла"
            });

            bool match1 = context.MatchPatternToString(new string[]{
                "это","какой-то","свин","вот"
            });
            bool match2 = context.MatchPatternToString(new string[]{
                "а","это","корова","и","корелла" ,"летят","на","юг"
            });

            bool match3 = context.MatchPatternToString(new string[]{
                "юг"
            });

            bool match4 = context.MatchPatternToString(new string[]{
                "корелла"
            });

            bool match5 = context.MatchPatternToString(new string[]{
                "корова"
            });

            Console.ReadLine();
        }
    }
}
