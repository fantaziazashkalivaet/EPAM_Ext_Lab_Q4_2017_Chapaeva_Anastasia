namespace Task01
{
    using System;
    using System.Linq;

    public class Logic
    {
        /// <summary>
        /// Ввод, проверка и запись координат
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        public static void SetCoordinates(out double x, out double y)
        {
            Console.WriteLine("Enter coordinates:");
            var coord = Console.ReadLine().Split(' ');
            double[] count = new double[2];
            if (coord.Count() == 2)
            {
                if (!double.TryParse(coord[0], out x) || !double.TryParse(coord[1], out y))
                {
                    Console.WriteLine("Incorrect data entered.");
                    SetCoordinates(out x, out y);
                }
            }
            else
            {
                Console.WriteLine("Incorrect coordinates entered.");
                SetCoordinates(out x, out y);
            }
        }

        /// <summary>
        /// Ввод, проверка и запись буквы графика (а-к)
        /// </summary>
        /// <returns>Вариант графика</returns>
        public static char SetOption()
        {
            Console.WriteLine("Введите букву графика (а-к):");
            string s = Console.ReadLine();
            char option = ' ';
            if (s.Length == 1)
            {
                option = s[0];
            }
            else
            {
                Console.WriteLine("Incorrect data entered.");
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
        public static void CheckSolution(double x, double y, char option)
        {
            switch (option)
            {
                case 'а':
                    {
                        if (Math.Abs((x * x) + (y * y)) <= 1)
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'б':
                    {
                        var r = Math.Abs((x * x) + (y * y));
                        if (r <= 1 && r >= 0.5)
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'в':
                    {
                        if (Math.Abs(x) <= 1 && Math.Abs(y) <= 1)
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'г':
                    {
                        if (y >= Math.Abs(x) - 1 && y <= -Math.Abs(x) + 1)
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'д':
                    {
                        if (y >= (2 * Math.Abs(x)) - 1 && y <= (-2 * Math.Abs(x)) + 1)
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'е':
                    {
                        if ((x >= -2 && x <= 0 && y <= (0.5 * x) + 1 && y >= (-0.5 * x) - 1) ||
                            (x > 0 && x <= 1 && Math.Abs((x * x) + (y * y)) <= 1))
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'ж':
                    {
                        if (y >= -1 && y <= 2 && y <= (2 * x) + 2 && y <= (-2 * x) + 2)
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'з':
                    {
                        if (y >= -2 && y <= 1 && y <= Math.Abs(x))
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'и':
                    {
                        if (y >= (x - 1) / 3 &&
                            ((x >= -2 && x <= 0 && y <= (2 * x) + 3 && y <= -x) || (x > 0 && x <= 1 && y <= 0)))
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                case 'к':
                    {
                        if (y >= 1 || (x >= -1 && x <= 1 && y >= Math.Abs(x)))
                        {
                            PrintResult(x, y, option, string.Empty);
                        }
                        else
                        {
                            PrintResult(x, y, option, "doesn't");
                        }

                        break;
                    }

                default:
                    Console.WriteLine("Incorrect data entered.");
                    CheckSolution(x, y, SetOption());
                    break;
            }
        }

        /// <summary>
        /// Вывод результата
        /// </summary>
        /// <param name="x">Координата Х</param>
        /// <param name="y">Координата Y</param>
        /// <param name="option">Вариант графика</param>
        /// <param name="str">Вывод "doesn't" в строке принадлежности</param>
        private static void PrintResult(double x, double y, char option, string str)
        {
            Console.WriteLine("The point ({0}, {1}) {2} belong to the figure [{3}].", x, y, str, option);
        }
    }
}
