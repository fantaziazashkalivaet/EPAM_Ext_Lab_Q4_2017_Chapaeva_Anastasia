namespace Task02
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var cki = new ConsoleKeyInfo();
            do
            {
                Console.Clear();

                Console.WriteLine("Enter a string to check if it is a positive number:");
                if (Console.ReadLine().IsPositiveInteger())
                {
                    Console.WriteLine("This string is positive number");
                }
                else
                {
                    Console.WriteLine("This string isn't positive number");
                }

                Console.WriteLine("\nPress any key to continue or ESC to exit");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
