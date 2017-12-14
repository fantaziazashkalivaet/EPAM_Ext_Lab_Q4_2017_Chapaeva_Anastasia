namespace Task03
{
    public class Rectangle : Figure
    {
        private double width;
        private double height;

        public Rectangle()
        {
            CentreX = 0;
            CentreY = 0;
            Width = 1;
            Height = 1;
        }

        public Rectangle(double centreX, double centreY, double width, double height)
        {
            CentreX = centreX;
            CentreY = centreY;
            Width = width;
            Height = height;
        }

        public double Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value > 0)
                {
                    width = value;
                }
                else
                {
                    width = 1;
                }
            }
        }

        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                if (value > 0)
                {
                    height = value;
                }
                else
                {
                    height = 1;
                }
            }
        }

        public override FigureType ReturnType()
        {
            return FigureType.Rectangle;
        }
    }
}
