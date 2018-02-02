// Написать методы поиска элементов в массиве

namespace Task03
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            const int StandardListSize = 500000;
            const int MinValueForRandom = -100;
            const int MaxValueForRandom = 100;

            var cki = new ConsoleKeyInfo();
            do
            {
                var rnd = new Random();
                var list = new List<int>();
                for (var i = 0; i < StandardListSize; i++)
                {
                    list.Add(rnd.Next(MinValueForRandom, MaxValueForRandom));
                }

                var check = new CheckTimeForFindPositive(list);
                check.Run();
                Console.WriteLine("\nPress any key to continue or ESC to exit");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
