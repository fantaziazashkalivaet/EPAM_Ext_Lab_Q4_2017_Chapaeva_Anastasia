﻿namespace Task04
{
    public abstract class Bonus : IBonus
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

        public abstract void Improve(Player player);
    }
}
