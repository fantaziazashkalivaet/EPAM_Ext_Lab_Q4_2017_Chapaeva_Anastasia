using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            var testArray = new int[] { 1, 2, 3, 4, 5 };
            Console.Write("Array: ");
            foreach(var e in testArray)
            {
                Console.Write("{0} ", e);
            }
            Console.WriteLine("\nSum: {0}", testArray.SumArray());
            Console.ReadKey();
        }
    }
}
