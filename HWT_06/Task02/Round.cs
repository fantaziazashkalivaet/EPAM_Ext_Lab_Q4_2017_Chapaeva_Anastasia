namespace Task02
{
    using System;

    public class Round
    {
        private double centreX;
        private double centreY;
        private double radius;
        private double circumference;
        private double area;

        public Round()
        {
            centreX = 1;
            centreY = 1;
            Radius = 1;
            circumference = CalculateCircumference(this.radius);
            area = CalculateArea(this.radius);
        }

        public Round(double centreX, double centreY, double radius)
        {
            this.centreX = centreX;
            this.centreY = centreY;
            Radius = radius;

            circumference = CalculateCircumference(this.radius);
            area = CalculateArea(this.radius);
        }

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

        public double Radius
        {
            get
            {
                return radius;
            }

            set
            {
                if (value < 0)
                {
                    radius = 0;
                }
                else
                {
                    radius = value;
                }

                circumference = CalculateCircumference(value);
                area = CalculateArea(value);
            }
        }

        public double Circumference
        {
            get
            {
                return circumference;
            }
        }

        public double Area
        {
            get
            {
                return area;
            }
        }

        protected double CalculateCircumference(double radius)
        {
            return 2 * Math.PI * radius;
        }

        protected double CalculateArea(double radius)
        {
            return Math.Pow(radius, 2) * Math.PI;
        }
    }
}
