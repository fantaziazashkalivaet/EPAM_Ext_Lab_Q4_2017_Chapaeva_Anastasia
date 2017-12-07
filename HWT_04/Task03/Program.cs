/*
 * Проведите сравнительный анализ скорости работы классов String и StringBuilder для операции сложения строк
 * */

namespace Task03
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var cki = new ConsoleKeyInfo();
            do
            {
                Logic.PrintResult();
                Console.WriteLine("\nPress any key to continue or ESC to exit");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
