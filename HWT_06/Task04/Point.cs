namespace Task04
{
    public class Point
    {
        private int x;
        private int y;

        public Point()
        {
            X = 0;//todo pn хардкод
            Y = 0;//todo pn хардкод
		}

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }
    }
}
