namespace Task02
{
    using System;

    public class Logic
    {
        /// <summary>
        /// Вычисление коэффициентов квадратного уравнения
        /// </summary>
        /// <param name="h">Параметр h</param>
        /// <param name="denominatorA">Знаменатель коэффициента а</param>
        /// <param name="a">Коэффициент a</param>
        /// <param name="b">Коэффициент b</param>
        /// <param name="c">Коэффициент c</param>
        public static void CalculationOfCoefficients(double h, double denominatorA, out double a, out double b, out double c)
        {
            var sqrH = h * h;
            a = Math.Sqrt((Math.Abs(Math.Sin(8 * h)) + 17) / Math.Pow(denominatorA, 2));
            b = 1 - Math.Sqrt(3 / (3 + Math.Abs(Math.Tan(a * sqrH) - Math.Sin(a * h))));
            c = (a * sqrH * Math.Sin(b * h)) + (b * h * sqrH * Math.Cos(a * h));
        }

        /// <summary>
        /// Решение квадратного уравнения
        /// </summary>
        /// <param name="a">Коэффициент a</param>
        /// <param name="b">Коэффициент b</param>
        /// <param name="c">Коэффициент c</param>
        public static void Solution(double a, double b, double c)
        {
            Console.WriteLine("Solution of the equation {0}*x^2 + {1}*x + ({2}) = 0 :", a, b, c);

            if (a == 0)
            {
                Console.WriteLine("a = 0, the equation is linear and has one root:");
                if (b == 0)
                {
                    Console.WriteLine("The equation hasn't roots");
                }
                else
                {
                    double x = -c / b;
                    Console.WriteLine("x = {0}", x);
                }
            }
            else
            {
                double d = (b * b) - (4 * a * c);
                Console.WriteLine("D = {0};", d);

                if (d >= 0)
                {
                    double sqrtD = Math.Sqrt(d);
                    double x1 = (-b + sqrtD) / (2 * a);
                    double x2 = (-b - sqrtD) / (2 * a);

                    Console.WriteLine("D > 0, the equation has two real roots:");
                    Console.WriteLine("x1 = {0}", x1);
                    Console.WriteLine("x2 = {0}", x2);
                }
                else
                {
                    Console.WriteLine("D < 0, the equation hasn't real roots.");
                }
            }
        }

        /// <summary>
        /// Ввод, проверка и запись значения
        /// </summary>
        /// <param name="name">Имя записываемой переменной</param>
        /// <returns>Считанное значение</returns>
        public static double SetValue(string name)
        {
            Console.WriteLine("Enter {0}:", name);
            double value;

            if (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetValue(name);
            }

            return value;
        }
    }
}
