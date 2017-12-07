namespace Task01
{
    using System;
    using System.Linq;

    public class Logic
    {
        public static void ReadAndCheckString(out bool exit)
        {
            exit = false;
            Console.WriteLine("Enter the string or \"exit\":");
            var str = Console.ReadLine().ToLower();

            switch (str)
            {
                case "exit":
                    {
                        exit = true;
                        break;
                    }

                case null:
                    {
                        PrintStringIsEmpty();
                        break;
                    }

                default:
                    {
                        var words = StringToWords(str);
                        var lettersInWord = GetLettersInWord(words);
                        var medianLength = MedianLength(lettersInWord);
                        var averageLength = AverageLength(lettersInWord);
                        Console.WriteLine("Average word length: {0}", averageLength);
                        Console.WriteLine("Median word length: {0}\n", medianLength);
                        break;
                    }
            }
        }

        public static void PrintStringIsEmpty()
        {
            Console.WriteLine("This string is empty.");
        }

        public static string[] StringToWords(string str)
        {
            var separators = new char[] { ' ', '\t' };
            return str.Split(separators);
        }

        public static int[] GetLettersInWord(string[] words)
        {
            var lettersInWord = new int[words.Count()];
            var iterator = 0;

            foreach (var word in words)
            {
                lettersInWord[iterator] = word.Count();
                foreach (var letter in word)
                {
                    if (char.IsPunctuation(letter))
                    {
                        lettersInWord[iterator]--;
                    }
                }

                iterator++;
            }

            return lettersInWord;
        }

        public static double MedianLength(int[] lettersInWord)
        {
            Array.Sort(lettersInWord);
            int center = (lettersInWord.Count() - 1) / 2;
            if (lettersInWord.Count() % 2 == 0)
            {
                return (double)(lettersInWord[center] + lettersInWord[center + 1]) / 2;
            }
            else
            {
                return lettersInWord[center];
            }
        }

        public static double AverageLength(int[] lettersInWord)
        {
            return (double)lettersInWord.Sum() / (double)lettersInWord.Count();
        }
    }
}
