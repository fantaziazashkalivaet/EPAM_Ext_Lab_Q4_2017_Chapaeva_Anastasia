namespace Task02
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class Logic
    {
        public static void ReadAndCheckString(out bool exit)
        {
            exit = false;

            Console.WriteLine("Enter the first string or \"exit\":");
            var strFirst = Console.ReadLine().ToLower();

            var str = SetUniqueSymbol(strFirst);

            switch (strFirst)
            {
                case "exit":
                    {
                        exit = true;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Enter the second string:");
                        var strSecond = new StringBuilder(Console.ReadLine().ToLower());

                        if (strSecond == null || strSecond.Length == 0)
                        {
                            Console.WriteLine("This string is empty.");
                            break;
                        }

                        foreach (var letter in str)
                        {
                            strSecond.Replace(string.Format("{0}", letter), string.Format("{0}{1}", letter, letter));
                        }

                        Console.WriteLine("New string: {0}", strSecond);
                        break;
                    }
            }
        }

        public static List<char> SetUniqueSymbol(string str)
        {
            var listOfSymbol = new List<char>();
            foreach (var symbol in str)
            {
                if (!listOfSymbol.Contains(symbol))
                {
                    listOfSymbol.Add(symbol);
                }
            }

            return listOfSymbol;
        }
    }
}
