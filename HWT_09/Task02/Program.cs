using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            string s1 = "-123";
            string s2 = "123";
            string s3 = "";
            string s4 = string.Empty;
            string s5 = "9d2";

            Console.WriteLine("{0} {1}", s1, s1.IsPositiveInteger());
            Console.WriteLine("{0} {1}", s2, s2.IsPositiveInteger());
            Console.WriteLine("{0} {1}", s3, s3.IsPositiveInteger());
            Console.WriteLine("{0} {1}", s4, s4.IsPositiveInteger());
            Console.WriteLine("{0} {1}", s5, s5.IsPositiveInteger());
            Console.ReadKey();
        }
    }
}
