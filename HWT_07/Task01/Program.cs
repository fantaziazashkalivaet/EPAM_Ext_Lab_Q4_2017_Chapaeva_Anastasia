namespace Task01
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            do
            {
                Console.Clear();
                ForConsole.PrintInfo();

                ForConsole.Write("Введите N:");
                var n = ForConsole.ReadInt();

                ForConsole.Write("Введите K:");
                var k = ForConsole.ReadInt();

                ForConsole.Write(string.Format("Последним останется " + Joseph.Solution(n, k) + "-й человек."));

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить или Escape (Esc), чтобы выйти:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
