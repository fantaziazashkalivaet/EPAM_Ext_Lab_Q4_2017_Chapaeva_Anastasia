namespace Task04
{
    public interface ICreature
    {
        void MoveRight(int widthMax);

        void MoveLeft(int widthMin);

        void MoveUp(int heightMax);

        void MoveDown(int heightMin);

        void Move(Side side);
    }
}
