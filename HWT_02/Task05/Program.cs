/*
Напишите программу, которая выводит на экран сумму всех чисел меньше 1000,
кратных 3, или 5.
 * */

namespace Task05
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            int n = 10;//todo pn задание не принято, не выполнено условие
            Console.WriteLine("Сумма всех чисел меньше 1000: {0}", Logic.Calc(n, 3, 5));
            Console.ReadKey();
		}
    }
}
