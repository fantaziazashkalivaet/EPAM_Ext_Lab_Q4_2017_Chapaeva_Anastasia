namespace Task02
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            const string TestText = "Text.txt";
            char[] separators = { ' ', '.', '\n', '\r' };
            var test = new TextAnalysis(TestText, separators);
            test.PrintDictionary();

            Console.WriteLine("\nPress any key to quit:");
            Console.ReadKey();
        }
    }
}
