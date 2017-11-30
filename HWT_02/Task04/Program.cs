/*
Написать программу, которая запрашивает с клавиатуры число N и
выводит на экран «елку», состоящую из N
треугольников
 * */

namespace Task04
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            do
            {
                int n = Logic.SetPositiveValue("N");
                Logic.PrintFir(n);

                Console.WriteLine("Press the Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);

            Console.ReadKey();
        }
    }
}
