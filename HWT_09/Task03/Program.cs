using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task03
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<int>();
            for(var i = -3; i < 4; i++)
            {
                list.Add(i);
            }

            FindPositive.Compare del = FindPositive.IsPositive;

            var a = FindPositive.FindPositiveSimple(list);
            var b = FindPositive.FindPositiveDelegate(list, del);
            var c = FindPositive.FindPositiveAnonDelegate(list);
            var d = FindPositive.FindPositiveLambdaDelegate(list);
            var e = FindPositive.FindPositiveLinqDelegate(list);
        }
    }
}
