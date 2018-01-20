namespace Task01
{
    using System;

    public class ForConsole
    {
        /// <summary>
        /// Вывод строки
        /// </summary>
        /// <param name="s">Выводимая строка</param>
        public static void Write(string s)
        {
            Console.WriteLine(s);
        }

        /// <summary>
        /// Считывание одного целого числа из консоли
        /// </summary>
        /// <returns>Возвращает считаное целое число</returns>
        public static int ReadInt()
        {
            int count;
            if (!int.TryParse(Console.ReadLine(), out count))
            {
                Write("Data isn't a number. Try again:");
                ReadInt();
            }

            return count;
        }

        /// <summary>
        /// Вывод условия задачи
        /// </summary>
        public static void PrintInfo()
        {
            Write("В кругу стоят N человек, пронумерованных от 1 до N.");
            Write("При ведении счета по кругу вычёркивается каждый K - й человек, пока не останется один.");
        }
    }
}
