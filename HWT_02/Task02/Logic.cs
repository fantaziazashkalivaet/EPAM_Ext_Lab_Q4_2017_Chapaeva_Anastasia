namespace Task02
{
    using System;

    public class Logic
    {
        /// <summary>
        /// Вывод треугольника
        /// </summary>
        /// <param name="n">Высота треугольника</param>
        public static void PrintTriangle(int n)
        {
            var stars = "*";

            for (var i = 0; i < n; i++)
            {
                Console.WriteLine(stars);
                stars += "*";
            }
        }

        /// <summary>
        /// Проверка и ввод положительного числа
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <returns>Введенное число</returns>
        public static int SetPositiveValue(string name)
        {
            int value = SetValue(name);

            if (value <= 0)
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetPositiveValue(name);
            }

            return value;
        }

        /// <summary>
        /// Проверка и ввод числа
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <returns>Введенное число</returns>
        public static int SetValue(string name)
        {
            Console.WriteLine("Enter {0}:", name);
            double value;

            if (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetValue(name);
            }

            return (int)value;
        }
    }
}
