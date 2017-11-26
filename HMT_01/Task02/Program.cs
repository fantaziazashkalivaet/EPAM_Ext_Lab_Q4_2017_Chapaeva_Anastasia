using System;

/*
Вывести действительные корни квадратного уравнения (если таковые имеются) при заданном числе h. 
Вывести промежуточные решения: a, b, c, D.
*/

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            double denominatorA;
            double h;

            do
            {
                h = SetValue("h");
                denominatorA = (1 - Math.Sin(4 * h) * Math.Cos(h * h + 18));
            } while (denominatorA == 0);

            var sqrH = h * h;
            double a = Math.Sqrt((Math.Abs(Math.Sin(8 * h))+17)/Math.Pow(denominatorA, 2));
            double b = 1 - Math.Sqrt(3 / (3 + Math.Abs(Math.Tan(a * sqrH) - Math.Sin(a * h))));
            double c = a * sqrH * Math.Sin(b * h) + b * h * sqrH * Math.Cos(a * h);

            Console.WriteLine("Решение уравнения {0}*x^2 + {1}*x + ({2}) = 0 :", a, b, c);

            double D = b * b - 4 * a * c;
            Console.WriteLine("D = {0};", D);
            if (D >= 0)
            {
                double sqrtD = Math.Sqrt(D);
                double x1 = (-b + sqrtD) / (2 * a);
                double x2 = (-b - sqrtD) / (2 * a);

                Console.WriteLine("D > 0, уравнение имеет два действительных корня:");
                Console.WriteLine("x1 = {0}", x1);
                Console.WriteLine("x2 = {0}", x2);
            }
            else
                Console.WriteLine("D < 0, уравнение не имеет действительных корней.");
        }

        /// <summary>
        /// Ввод, проверка и запись значения
        /// </summary>
        /// <param name="name">Имя записываемой переменной</param>
        /// <returns></returns>
        static double SetValue(string name)
        {
            Console.WriteLine("Введите значение {0}:", name);
            double value;
            if (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Введены неверные данные.");
                value = SetValue(name);
            }
            return value;
        }
    }
}
