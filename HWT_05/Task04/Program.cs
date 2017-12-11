namespace Task04
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            // WARNING очень жуткая демонстрация
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

                char symbolOld = 'o';
                Console.WriteLine("\nFirst occurrence of a symbol '{0}' in a string \"{1}\": {2}", symbolOld, newString.ToString(), newString.IndexOf(symbolOld));

                char symbolNew = 'n';
                var newString1 = newString.Replace(symbolNew, symbolOld);
                Console.WriteLine(
                    "Replace the symbol '{0}' -> '{1}' in string \"{2}\": {3}",
                    symbolOld,
                    symbolNew,
                    newString,
                    newString1);

                int position = 0;
                string addStr = "add substring";
                var newString2 = newString.Insert(addStr, position);
                Console.WriteLine("\nAdd a substring \"{0}\" in string \"{1}\" from the position '{2}': {3}", addStr, newString.ToString(), position, newString2.ToString());

                var newString3 = newString.Split();
                Console.WriteLine("\nSplit a string \"{0}\":", newString.ToString());
                foreach (var e in newString3)
                {
                    Console.WriteLine("\t\"{0}\",", e.ToString());
                }

                Console.WriteLine("\nString to lower: {0}", newString.ToLower().ToString());
                Console.WriteLine("String to lower: {0}", newString.ToUpper().ToString());

                Console.WriteLine("Press the Escape (Esc) key to quit:");
                cki = Console.ReadKey();
            }
            while (cki.Key != ConsoleKey.Escape);
        }
    }
}
