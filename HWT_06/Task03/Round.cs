namespace Task03
{
    using System;

    public class Round : Circle
    {
        public Round() : base()
        {
        }

        public Round(double centreX, double centreY, double radius) : base(centreX, centreY, radius)
        {
        }

        public override FigureType ReturnType()
        {
            return FigureType.Round;
        }

        public override bool IsInside(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2) <= Math.Pow(Radius, 2);
        }

        public override string Info()
        {
            return string.Format("{0}: centre = ({1}, {2}), radius = {3}", this.ReturnType(), CentreX, CentreY, Radius);
        }
    }
}
