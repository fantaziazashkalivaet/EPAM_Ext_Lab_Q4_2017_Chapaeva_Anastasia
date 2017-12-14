namespace Task03
{
    public abstract class Figure
    {
        private double centreX;
        private double centreY;

        public double CentreX
        {
            get
            {
                return centreX;
            }

            set
            {
                centreX = value;
            }
        }

        public double CentreY
        {
            get
            {
                return centreY;
            }

            set
            {
                centreY = value;
            }
        }

        public virtual FigureType ReturnType()
        {
                return FigureType.Figure;
        }

        public abstract string Info();
    }
}
