/*
Написать консольное приложение, которое проверяет
принадлежность точки заштрихованной области.
Пользователь вводит координаты точки (x; y) и выбирает
букву графика (a-к). В консоли должно высветиться сообщение:
«Точка [(x; y)] принадлежит фигуре [г]».
*/

namespace Task01
{
    using System;

    public class Program
    {
        private static void Main(string[] args)
        {
            ConsoleKeyInfo cki;
            do
            {
                double x, y;
                Logic.SetCoordinates(out x, out y);
                char option = Logic.SetOption();

                Logic.CheckSolution(x, y, option);

                Console.WriteLine("Press the Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
            Console.ReadKey();
        }
    }
}
