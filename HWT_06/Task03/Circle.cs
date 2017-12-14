namespace Task03
{
    using System;

    public class Circle : Figure
    {
        private double radius;

        public Circle()
        {
            this.CentreX = 0;
            this.CentreY = 0;
            radius = 1;
        }

        public Circle(double centreX, double centreY, double radius)
        {
            this.CentreX = centreX;
            this.CentreY = centreY;
            this.radius = radius;
        }

        public double Radius
        {
            get
            {
                return radius;
            }

            set
            {
                if (value > 0)
                {
                    radius = value;
                }
                else
                {
                    radius = 1;
                }
            }
        }

        public override FigureType ReturnType()
        {
            return FigureType.Circle;
        }

        public virtual bool IsInside(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2) == Math.Pow(Radius, 2);
        }

        public override string Info()
        {
            return string.Format("{0}: centre = ({1}, {2}), radius = {3}", this.ReturnType(), CentreX, CentreY, Radius);
        }
    }
}
