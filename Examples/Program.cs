using System;
using SamplesToTextsMatcher;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            TimeSpan ts;

            //string pattern = "свин | корелла & корова";
            //string pattern = "(свин | корелла) & корова";
            //string pattern = "((свин | корелл*) /2 (=корова | \"оранжевый бобер\")) ~какаду";
            string pattern = "\"мокрая корелла\" & волнистый | (корабли /4 (рыбки | рыбок | рыбками | водоросли))";

            string pat = "((\"мокрая корелла\" & (((((((((((((волнистый | волнистый) | волнистого) | волнистому) | волнистым) | волнистом) | волнистая) | волнистой) | волнистую) | волнистою) | волнистое) | волнистые) | волнистых) | волнистыми)) | (((((((((((корабли | корабль) | корабля) | кораблю) | кораблём) | корабле) | корабли) | кораблей) | кораблям) | кораблями) | кораблях) /4 (((((((((((((рыбки | рыбка) | рыбки) | рыбке) | рыбку) | рыбкой) | рыбкою) | рыбок) | рыбкам) | рыбками) | рыбках) | ((((((((((рыбок | рыбка) | рыбки) | рыбке) | рыбку) | рыбкой) | рыбкою) | рыбок) | рыбкам) | рыбками) | рыбках)) | ((((((((((рыбками | рыбка) | рыбки) | рыбке) | рыбку) | рыбкой) | рыбкою) | рыбок) | рыбкам) | рыбками) | рыбках)) | (((((((водоросли | водоросль) | водоросли) | водорослью) | водорослей) | водорослям) | водорослями) | водорослях))))";

            Context ctx = new Context(pat, new ConcretePatternParser());

            var dict = new ConcreteMorfDictionary();
            Context context = new Context(pattern, new ConcretePatternParser(), dict);


            stopWatch.Start();
            for(int i =0; i <1000; i++)
            {
                bool match7 = context.MatchPatternToString(new string[]{
                    "корова", "и", "и", "\"мокрая корелла\"", "и", "волнистый",
                    "корова", "и", "и", "\"мокрая корелла\"", "и", "волнистый",
                    "корова", "и", "и", "\"мокрая корелла\"", "и", "волнистый",
                    "корова", "и", "и", "\"мокрая корелла\"", "и", "волнистый",
                    "корова", "и", "и", "\"мокрая корелла\"", "и", "волнистый",
                    "корова", "и", "и", "\"мокрая корелла\"", "и", "волнистый"
                });
            }
            
            
            stopWatch.Stop();
            ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);

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
