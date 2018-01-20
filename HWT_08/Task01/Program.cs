// Написать программу, выполняющую сортировку массива строк по возрастанию длины.
// Если строки состоят из равного числа символов, их следует отсортировать по алфавиту.
// Реализовать метод сравнения строк отдельным методом,
// передаваемым в сортировку через делегат.

namespace Task01
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            do
            {
                Console.Clear();
                Console.WriteLine("Enter lines:");

                string newLine;
                var linesList = new List<string>();
                while ((newLine = Console.ReadLine()) != string.Empty)
                {
                    linesList.Add(newLine);
                }

                string[] linesArray = linesList.ToArray();

                CompareString.Sort(linesArray, CompareString.Compare);

                Console.WriteLine("\nSorted array of lines:");
                foreach (var e in linesArray)
                {
                    Console.WriteLine(e);
                }

                Console.WriteLine("Press Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
