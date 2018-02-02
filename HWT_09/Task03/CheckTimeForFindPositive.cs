// WARNING: ОЧЕНЬ МНОГО ПОВТОРЕНИЯ КОДА
namespace Task03
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class CheckTimeForFindPositive
    {
        private const int NumberOfTests = 100;

        private List<int> arr;

        public CheckTimeForFindPositive(List<int> arr)
        {
            this.arr = arr;
        }

        public void Run()
        {
            Console.WriteLine("Initial sequence has {0} elements, ", arr.Count);
            Console.WriteLine("Number of checks: {0}\n", NumberOfTests);

            Console.WriteLine("Time analysis:");

            PrintResult(MedianeTimeFindSimple(), "Simple"); // куда вынести эти названия?
            PrintResult(MedianeTimeFindWithDelegate(), "With delegate");
            PrintResult(MedianeTimeFindWithAnonDelegate(), "With anonymous delegate");
            PrintResult(MedianeTimeFindWithLambdaDelegate(), "With Lambda delegate");
            PrintResult(MedianeTimeFindWithLinqDelegate(), "With Linq delegate");
        }

        private long MedianeTimeFindSimple()
        {
            var times = new List<long>();
            for (var i = 0; i < NumberOfTests; i++)
            {
                var watch = new Stopwatch();
                watch.Start();
                var newList = FindPositive.FindPositiveSimple(arr);
                times.Add(watch.ElapsedMilliseconds);
                watch.Stop();
            }

            return MedianeTimeInList(times);
        }

        private long MedianeTimeFindWithDelegate()
        {
            var times = new List<long>();
            for (var i = 0; i < NumberOfTests; i++)
            {
                var watch = new Stopwatch();
                watch.Start();
                FindPositive.Compare del = FindPositive.IsPositive;
                var newList = FindPositive.FindPositiveDelegate(arr, del);
                times.Add(watch.ElapsedMilliseconds);
                watch.Stop();
            }

            return MedianeTimeInList(times);
        }

        private long MedianeTimeFindWithAnonDelegate()
        {
            var times = new List<long>();
            for (var i = 0; i < NumberOfTests; i++)
            {
                var watch = new Stopwatch();
                watch.Start();
                var newList = FindPositive.FindPositiveAnonDelegate(arr);
                times.Add(watch.ElapsedMilliseconds);
                watch.Stop();
            }

            return MedianeTimeInList(times);
        }

        private long MedianeTimeFindWithLambdaDelegate()
        {
            var times = new List<long>();
            for (var i = 0; i < NumberOfTests; i++)
            {
                var watch = new Stopwatch();
                watch.Start();
                var newList = FindPositive.FindPositiveLambdaDelegate(arr);
                times.Add(watch.ElapsedMilliseconds);
                watch.Stop();
            }

            return MedianeTimeInList(times);
        }

        private long MedianeTimeFindWithLinqDelegate()
        {
            var times = new List<long>();
            for (var i = 0; i < NumberOfTests; i++)
            {
                var watch = new Stopwatch();
                watch.Start();
                var newList = FindPositive.FindPositiveLinqDelegate(arr);
                times.Add(watch.ElapsedMilliseconds);
                watch.Stop();
            }

            return MedianeTimeInList(times);
        }

        private void PrintResult(long time, string methodName)
        {
            Console.WriteLine("The average value for the method \"{0}\": {1}ms", methodName, time);
        }

        private long MedianeTimeInList(List<long> times)
        {
            return times.Sum() / times.Count;
        }
    }
}
