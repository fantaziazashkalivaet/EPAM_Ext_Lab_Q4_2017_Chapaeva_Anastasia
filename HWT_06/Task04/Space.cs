namespace Task04
{
    public abstract class Space
    {
        private Point position;

        public Point Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public abstract bool IsMove();
    }
}
