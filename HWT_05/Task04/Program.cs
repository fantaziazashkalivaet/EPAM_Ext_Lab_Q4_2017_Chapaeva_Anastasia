namespace Task04
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            ConsoleKeyInfo cki;

            do
            {
                Console.WriteLine("Enter first string:");
                var first = new MyString(Console.ReadLine().ToCharArray());
                Console.WriteLine("\t length: {0}", first.Length);

                Console.WriteLine("\nEnter second string:");
                var second = new MyString(Console.ReadLine().ToCharArray());
                Console.WriteLine("\t length: {0}", second.Length);

                MyString newString = first + second;
                Console.WriteLine("\nJoin two lines: {0}", newString.ToString());
                Console.WriteLine("\t length: {0}", newString.Length);

                newString = newString.Trim();
                Console.WriteLine("\nDelite separator from the beginning and end: {0}", newString.ToString());
                Console.WriteLine("\t length: {0}", newString.Length);

                Console.WriteLine("\nString to lower: {0}", newString.ToLower().ToString());
                Console.WriteLine("String to lower: {0}", newString.ToUpper().ToString());

                Console.WriteLine("Press the Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
