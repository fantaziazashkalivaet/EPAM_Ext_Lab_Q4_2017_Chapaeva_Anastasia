namespace Task04
{
    using System;

    public abstract class Creature : ICreature
    {
        private Point positions;
        private int step;
        private int health;

        public Point Positions
        {
            get
            {
                return positions;
            }

            set
            {
                positions = value;
            }
        }

        public int Step
        {
            get
            {
                return step;
            }

            set
            {
                step = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public abstract void Move(Side side);

        public void MoveDown(int heightMin)
        {
            throw new NotImplementedException();
        }

        public void MoveLeft(int widthMin)
        {
            throw new NotImplementedException();
        }

        public void MoveRight(int widthMax)
        {
            throw new NotImplementedException();
        }

        public void MoveUp(int heightMax)
        {
            throw new NotImplementedException();
        }
    }
}
