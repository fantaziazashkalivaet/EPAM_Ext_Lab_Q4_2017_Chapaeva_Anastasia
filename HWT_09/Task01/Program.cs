namespace Task01
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var testArray = new int[] { 1, 2, 3, 4, 5 };
            Console.Write("Array: ");
            foreach (var e in testArray)
            {
                Console.Write("{0} ", e);
            }

            Console.WriteLine("\nSum: {0}", testArray.SumArray());
            Console.ReadKey();
        }
    }
}
