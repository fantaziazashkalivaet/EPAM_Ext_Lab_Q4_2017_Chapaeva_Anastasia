namespace Task03
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;
            var array = Helper.SetArray();
            bool isNewCreate = false;

            while (!exit)
            {
                if (isNewCreate)
                {
                    array = Helper.SetArray();
                }

                Helper.PrintArray(array);
                int? sum = Helper.SumNotNeg(array);

                if (sum != null)
                {
                    Console.WriteLine("Sum of non-negative elements of the array:{0}", sum);
                }
               
                exit = Helper.ReadCommand(array, out isNewCreate);
            }
        }
    }
}
