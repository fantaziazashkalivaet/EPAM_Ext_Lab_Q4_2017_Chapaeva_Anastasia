namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args)
        {
            var smthg = new DynamicArray<int>();
            smthg = new DynamicArray<int>(7);
            for (var i = 0; i < smthg.Length; i++)
            {
                smthg[i] = i;
            }

            smthg.Add(7);
            smthg.Remove(5);
            smthg.Insert(0, 10);
            smthg.Insert(7, 11);
            smthg.Remove(11);

            Console.ReadKey();
        }
    }
}
