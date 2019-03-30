using Lab.LabirinthModel;

namespace Lab
{
    public class Ground : Cell
    {
        public Ground(int x, int y) : base(x, y, ' ') { }

        public override bool CanStep(Maze maze)
        {
            return true;
        }
    }
}