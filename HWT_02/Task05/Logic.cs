namespace Task05
{
    using System.Collections.Generic;
    using System.Linq;

    public class Logic
    {
        public static int Calc(int n)
        {
            var listAllElem = new List<int>();
            int dividThree = 3;
            int dividFive = 5;

            for (; dividThree < n || dividFive < n; dividThree += 3, dividFive += 5)
            {
                if (!listAllElem.Contains(dividThree))
                {
                    listAllElem.Add(dividThree);
                }

                if (!listAllElem.Contains(dividFive) && dividFive < n)
                {
                    listAllElem.Add(dividFive);
                }
            }

            IEnumerable<int> listElem = listAllElem.Distinct();

            int count = 0;

            foreach (var element in listElem)
            {
                count += element;
            }

            return count;
        }
    }
}
