namespace Task04
{
    public abstract class Сreature //todo pn ещё один такой же класс? для чего
	{
        private const int Step = 1;
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

        public void MoveRight(int widthMax)
        {
            if (position.X <= widthMax - Step)
            {
                position.X += Step;
            }
        }

        public void MoveLeft(int widthMin)
        {
            if (position.X >= widthMin + Step)
            {
                position.X -= Step;
            }
        }

        public void MoveUp(int heightMax)
        {
            if (position.Y <= heightMax - Step)
            {
                position.Y += Step;
            }
        }

        public void MoveDown(int heightMin)
        {
            if (position.Y <= heightMin + Step)
            {
                position.Y -= Step;
            }
        }

        public void Move(Side side)
        {
            // всякие проверки
        }
    }
}
