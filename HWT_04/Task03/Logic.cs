namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;

    public class Logic
    {
        public static List<double> WatchString(int quantityIter, int quantityOper)
        {
            string str = string.Empty;
            int n = 1;
            var logs = new List<double>();

            while (n <= quantityOper)
            {
                var watch = new Stopwatch();
                watch.Start();

                for (var j = 0; j < quantityIter; j++)
                {
                    str += "*";//todo pn хардкод
                }

                watch.Stop();
                logs.Add(watch.ElapsedMilliseconds);
                n++;
            }

            return logs;
        }

        public static List<double> WatchStringBuilder(int quantityIter, int quantityOper)
        {
            var str = new StringBuilder();
            int n = 1;
            var logs = new List<double>();

            while (n <= quantityOper)
            {
                var watch = new Stopwatch();
                watch.Start();

                for (var j = 0; j < quantityIter; j++)
                {
                    str.Append("*");//todo pn хардкод
				}

                watch.Stop();
                logs.Add(watch.ElapsedMilliseconds);
                n++;
            }

            return logs;
        }

        public static void PrintLogs(List<double> logs, int quantityIter, int quantityOper)
        {
            int n = 1;
            foreach (var log in logs)
            {
                Console.WriteLine("\t{0} test: {1} ms / {2} operations;", n, log, quantityIter);
                n++;
            }
        }

        public static double CalcAverageTime(List<double> logs)
        {
            return logs.Sum() / logs.Count();
        }

        public static void PrintResult()
        {
            int quantityIter = 10000;//todo pn хардкод
			int quantityOper = 10;//todo pn хардкод

			Console.WriteLine("String:");

            var timeString = WatchString(quantityIter, quantityOper);
            PrintLogs(timeString, quantityIter, quantityOper);

            Console.WriteLine("\nStringBuilder:");

            var timeStringBuilder = WatchStringBuilder(quantityIter, quantityOper);
            PrintLogs(timeStringBuilder, quantityIter, quantityOper);

            Console.WriteLine("\nAverage time for string: {0}", CalcAverageTime(timeString));
            Console.WriteLine("Average time for StringBuilder: {0}", CalcAverageTime(timeStringBuilder));
        }
    }
}
