namespace Task03
{
    public class Line : Figure
    {
        private double length;

        public Line()
        {
            CentreX = 0;
            CentreY = 0;
            Length = 1;
        }

        public Line(double centreX, double centreY, double length)
        {
            CentreX = centreX;
            CentreY = centreY;
            Length = length;
        }

        public double Length
        {
            get
            {
                return length;
            }

            set
            {
                if (value > 0)
                {
                    length = value;
                }
                else
                {
                    length = 1;
                }
            }
        }

        public override FigureType ReturnType()
        {
            return FigureType.Line;
        }

        public override string Info()
        {
            return string.Format("{0}: centre = ({1}, {2}), length = {3}", this.ReturnType(), CentreX, CentreY, Length);
        }
    }
}
