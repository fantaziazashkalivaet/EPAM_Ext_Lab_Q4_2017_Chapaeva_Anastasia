namespace Task04
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;
            var array = Helper.CreateArray();
            bool isNewCreate = false;

            while (!exit)
            {
                if (isNewCreate)
                {
                    array = Helper.CreateArray();
                }

                Helper.PrintArray(array);
                int? sum = Helper.SumPos(array);

                if (sum != null)
                {
                    Console.WriteLine("Sum of non-negative elements of the array:{0}", sum);
                }

                exit = Helper.ReadCommand(array, out isNewCreate);
            }
        }
    }
}
