namespace Task01
{
    using System;

    public class Logic
    {
        /// <summary>
        /// Вычисление площади прямоугольника
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        /// <returns></returns>
        public static double AreaCalc(double width, double height)
        {
            return width * height;
        }

        /// <summary>
        /// Проверка числа (>0)
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <returns></returns>
        public static double SetPositiveValue(string name)
        {
            double value = SetValue(name);

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
