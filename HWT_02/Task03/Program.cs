/*
Написать программу, которая запрашивает с клавиатуры число N и
выводит на экран равносторонний треугольник из "*", состоящий из N строк
*/

namespace Task03
{
    using System;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            do
            {
                int n = Logic.SetPositiveValue("N");
                Logic.PrintTriangle(n);

                Console.WriteLine("Press the Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);

            Console.ReadKey();//todo pn лишнее
		}
    }
}
