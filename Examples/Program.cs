using System;
using SamplesToTextsMatcher;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = "свин | корелла";
            Context context = new Context(pattern, null);

            bool parse1 = context.MatchPatternToString("это какой-то свин вот");
            bool parse2 = context.MatchPatternToString("а это корелла летит");

            Console.ReadLine();
        }
    }
}
