namespace Task04
{
    public class Point
    {
        private int x;
        private int y;
        private TypePoint type;

        public Point()
        {
            X = 0;
            Y = 0;
            Type = TypePoint.obctacle;
        }

        public Point(int x, int y, TypePoint type)
        {
            X = x;
            Y = y;
            Type = type;
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

        public TypePoint Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }
    }
}
