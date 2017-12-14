namespace Task02
{
    public class Ring : Round
    {
        private double innerRadius;
        private double innerCircumference;
        private double innerArea;
        private double ringCircumference;
        private double ringArea;

        public Ring() : base()
        {
            InnerRadius = 0;//todo pn hardcode
        }

        public Ring(double centreX, double centreY, double radius, double innerRadius) : base(centreX, centreY, radius)
        {
            InnerRadius = innerRadius;
        }

        public double InnerRadius
        {
            get
            {
                return innerRadius;
            }

            set
            {
                if (value < 0)
                {
                    innerRadius = 0;
                }
                else
                {
                    innerRadius = value;
                }

                innerCircumference = CalculateCircumference(value);
                innerArea = CalculateArea(value);
                CalculateRingCircumference();
                CalculateRingArea();
            }
        }

        public double InnerCircumference
        {
            get
            {
                return innerCircumference;
            }
        }

        public double InnerArea
        {
            get
            {
                return innerArea;
            }
        }

        public double RingCircumference
        {
            get
            {
                return ringCircumference;
            }
        }

        public double RingArea
        {
            get
            {
                return ringArea;
            }
        }

        protected void CalculateRingCircumference()
        {
            ringCircumference = Circumference - innerCircumference;
        }

        protected void CalculateRingArea()
        {
            ringArea = Area - innerArea;
        }
    }
}
