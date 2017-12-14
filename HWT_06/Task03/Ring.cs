namespace Task03
{
    using System;

    public class Ring : Circle
    {
        private double outerRadius;

        public Ring() : base()
        {
            OuterRadius = 2;
        }

        public Ring(double centreX, double centreY, double innerRadius, double outerRadius)
        {
            CentreX = centreX;
            CentreY = centreY;
            Radius = innerRadius;
            OuterRadius = outerRadius;
        }

        public double OuterRadius
        {
            get
            {
                return outerRadius;
            }

            set
            {
                if (value > 0 && value > Radius)
                {
                    outerRadius = value;
                }
                else
                {
                    outerRadius = Radius + 1;
                }
            }
        }

        public override FigureType ReturnType()
        {
            return FigureType.Ring;
        }

        public override bool IsInside(double x, double y)
        {
            double distance = Math.Sqrt((x * x) - (y * y));
            return Radius <= distance && distance <= OuterRadius;
        }

        public override string Info()
        {
            return string.Format("{0}: centre = ({1}, {2}), outer radius = {3}, inner radius = {4}",
                this.ReturnType(),
                CentreX,
                CentreY,
                OuterRadius,
                Radius);
        }
    }
}
