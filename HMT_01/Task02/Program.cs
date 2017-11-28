/*
Вывести действительные корни квадратного уравнения (если таковые имеются) при заданном числе h. 
Вывести промежуточные решения: a, b, c, D.
*/

namespace Task02
{
    using System;

    public class Program
    {
        private static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            do
            {
                double denominatorA;
                double h;

                do
                {
                    h = Logic.SetValue("h");
                    denominatorA = Math.Pow(1 - (Math.Sin(4 * h) * Math.Cos((h * h) + 18)), 2);
                }
                while (denominatorA == 0);

                double a, b, c;
                Logic.CalculationOfCoefficients(h, denominatorA, out a, out b, out c);

                Logic.Solution(a, b, c);

                Console.WriteLine("Press the Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);

            Console.ReadKey();
        }
    }
}
