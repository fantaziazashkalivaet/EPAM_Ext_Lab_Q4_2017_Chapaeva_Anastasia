using System;
using System.Linq;

/*
Написать консольное приложение, которое проверяет
принадлежность точки заштрихованной области.
Пользователь вводит координаты точки (x; y) и выбирает
букву графика (a-к). В консоли должно высветиться сообщение:
«Точка [(x; y)] принадлежит фигуре [г]».
*/

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] coordinates = SetCoordinates();
            var x = coordinates[0];
            var y = coordinates[1];
            char option = SetOption();

            CheckSolution(x, y, option);
        }

        /// <summary>
        /// Вывод результата, если точка принадлежит области
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="option">Вариант графика</param>
        static void PrintResultCorrect(double x, double y, char option)
        {
            Console.WriteLine("Точка ({0}, {1}) принадлежит фигуре [{2}].", x, y, option);
        }

        /// <summary>
        /// Вывод результата, если точка не принадлежит области
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="option">Вариант графика</param>
        static void PrintResultWrong(double x, double y, char option)
        {
            Console.WriteLine("Точка ({0}, {1}) не принадлежит фигуре [{2}].", x, y, option);
        }

        /// <summary>
        /// Ввод, проверка и запись координат
        /// </summary>
        /// <returns></returns>
        static double[] SetCoordinates()
        {
            Console.WriteLine("Введите координаты через пробел:");
            var coord = Console.ReadLine().Split(' ');
            double[] count = new double[2];
            if (coord.Count() == 2)
            {
                if (!double.TryParse(coord[0], out count[0]) || !double.TryParse(coord[1], out count[1]))
                {
                    Console.WriteLine("Введены неверные данные.");
                    count = SetCoordinates();
                }

            }
            else
            {
                Console.WriteLine("Введены неверные координаты.");
                count = SetCoordinates();
            }

            return count;
        }

        /// <summary>
        /// Ввод, проверка и запись буквы графика (а-к)
        /// </summary>
        /// <returns></returns>
        static char SetOption()
        {
            Console.WriteLine("Введите букву графика (а-к):");
            string s = Console.ReadLine();
            char option = ' ';
            if (s.Length == 1)
               option = s[0];
            else
            {
                Console.WriteLine("Введено неверное значение.");
                option = SetOption();
            }

            return option;
        }

        /// <summary>
        /// Проверка принадлежности точки (x, y) к выбранной области графика
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <param name="option">Вариант графика</param>
        static void CheckSolution(double x, double y, char option)
        {
            switch (option)
            {
                case 'а':
                    {
                        if (Math.Abs(x * x + y * y) <= 1)
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'б':
                    {
                        var r = Math.Abs(x * x + y * y);
                        if (r <= 1 && r >= 0.5)
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'в':
                    {
                        if (Math.Abs(x) <= 1 && Math.Abs(y) <= 1)
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'г':
                    {
                        if (y >= Math.Abs(x) - 1 && y <= -Math.Abs(x) + 1)
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'д':
                    {
                        if (y >= 2 * Math.Abs(x) - 1 && y <= -2 * Math.Abs(x) + 1)
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'е':
                    {
                        if ((x >= -2 && x <= 0 && y <= 0.5 * x + 1 && y >= -0.5 * x - 1) ||
                            (x > 0 && x <= 1 && Math.Abs(x * x + y * y) <= 1))
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'ж':
                    {
                        if (y >= -1 && y <= 2 && y <= 2 * x + 2 && y <= -2 * x + 2)
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'з':
                    {
                        if (y >= -2 && y <= 1 && y <= Math.Abs(x))
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'и':
                    {
                        if (y >= (x - 1) / 3 &&
                            ((x >= -2 && x <= 0 && y <= 2 * x + 3 && y <= -x) || (x > 0 && x <= 1 && y <= 0)))
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                case 'к':
                    {
                        if (y >= 1 || (x >= -1 && x <= 1 && y >= Math.Abs(x)))
                            PrintResultCorrect(x, y, option);
                        else PrintResultWrong(x, y, option);
                        break;
                    }
                default:
                    Console.WriteLine("Введено неверное значение.");
                    option = SetOption();
                    break;
            }
        }
    }
}
