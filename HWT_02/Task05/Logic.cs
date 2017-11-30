namespace Task05
{
    public class Logic
    {
        public static int Calc(int n, int dividThree, int dividFive)
        {
            int count = SumDiv(dividThree, n);
            count += SumDiv(dividFive, n);
            count -= SumDiv(dividThree * dividFive, n);

            return count;
        }

        public static int SumDiv(int div, int end)
        {
            int sum = 0;
            for (var i = div; i < end; i += div)
            {
                sum += i;
            }

            return sum;
        }
    }
}
