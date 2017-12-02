/*
 * Написать программу, которая определяет площадь прямоугольника со
сторонами a и b. Если пользователь вводит некорректные значения
(отрицательные, или 0), должно выдаваться сообщение об ошибке.
 * */

namespace Task01
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            do
            {
                double width = Logic.SetPositiveValue("width");
                double height = Logic.SetPositiveValue("height");
                double area = Logic.AreaCalc(width, height);

                Console.WriteLine("Rectangle: width = {0}, height = {1}, area = {2}", width, height, area);

                Console.WriteLine("Press the Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
            Console.ReadKey();//todo pn лишнее
		}
    }
}
