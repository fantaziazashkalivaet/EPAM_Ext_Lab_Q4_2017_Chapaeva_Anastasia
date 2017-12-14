// Напишите заготовку для векторного графического редактора.
// Полная версия редактора должна позволять создавать и выводить на экран такие фигуры как:
// Линия, Окружность, Прямоугольник, Круг, Кольцо.

namespace Task03
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;
            var figures = new Figures();

            while (!exit)
            {
                ForConsole.PrintMenu();
                exit = ForConsole.CheckCommand(ref figures);
            }

            foreach (var figure in figures.Lines)
            {
                Console.WriteLine(figure.Info());
            }

            foreach (var figure in figures.Rectangles)
            {
                Console.WriteLine(figure.Info());
            }

            foreach (var figure in figures.Circles)
            {
                Console.WriteLine(figure.Info());
            }

            foreach (var figure in figures.Rounds)
            {
                Console.WriteLine(figure.Info());
            }

            foreach (var figure in figures.Rings)
            {
                Console.WriteLine(figure.Info());
            }

            Console.ReadKey();
        }
    }
}
