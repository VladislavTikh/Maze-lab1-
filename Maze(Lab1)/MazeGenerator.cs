using Lab.LabirinthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab
{
    public class MazeGenerator
    {
        private Maze _maze;
        private List<Cell> _wallsToRemove = new List<Cell>();
        private int Width { get; set; }
        private int Height { get; set; }
        private Random rand = new Random();

        /// <summary>
        /// Use default width == 8 and height == 8
        /// </summary>
        public MazeGenerator() : this(8, 8)
        {
        }

        /// <summary>
        /// You can pass custom width and height
        /// </summary>
        /// <param name="width">width of labirinth</param>
        /// <param name="height">height of labirinth</param>
        public MazeGenerator(int width, int height)
        {
            Width = width;
            Height = height;
        }
        /// <summary>
        /// returns created maze
        /// </summary>
        /// <returns></returns>
        public Maze GetLabirinth()
        {
            _maze = new Maze(Width, Height);
            Step(_maze.Cells[0]);
            SetCharges();
            SpawnBoss();
            return _maze;
        }
        private void SpawnBoss()
        {
            var randCell= GetRandomCell(_maze.Cells.OfType<Ground>().ToList());
            var boss = new Enemy(randCell.X, randCell.Y);
            _maze._boss = boss;
            _maze[randCell.X, randCell.Y] = _maze._boss;
        }

        private void SetCharges()
        {
            var count=rand.Next(4, 8);
            for (var i = 0; i < count; i++)
            {
                var randGround = GetRandomCell(_maze.Cells.OfType<Ground>().ToList());
                _maze[randGround.X, randGround.Y] = new Charge(randGround.X, randGround.Y);
            }
        }

        private void Step(Cell cellToRemove)
        {
            _maze[cellToRemove.X, cellToRemove.Y] = new Ground(cellToRemove.X, cellToRemove.Y);
            _wallsToRemove.Remove(cellToRemove);

            var nearWalls = GetNearCell<Wall>(cellToRemove);

            var nearDonotRemove = nearWalls.Where(x => !IsRemovable(x));
            _wallsToRemove.RemoveAll(wallToRem => 
                nearDonotRemove.Any(wallNoRem => wallToRem.X == wallNoRem.X && wallToRem.Y == wallNoRem.Y));

            _wallsToRemove.AddRange(nearWalls.Where(IsRemovable));

            if (!_wallsToRemove.Any())
            {
                return;
            }

            var nextCellToRemove = GetRandomCell(_wallsToRemove);

            Step(nextCellToRemove);
        }

        private bool IsRemovable(Wall wall)
        {
            if (wall == null)
            {
                return false;
            }

            if (GetNearCell<Cell>(wall).Count(x => !(x is Wall)) > 1)
            {
                return false;
            }

            return true;
        }

        private List<T> GetNearCell<T>(Cell cellToRemove) where T : Cell
        {
            var nearCells = new List<Cell>();

            nearCells.Add(_maze[cellToRemove.X + 1, cellToRemove.Y]);
            nearCells.Add(_maze[cellToRemove.X - 1, cellToRemove.Y]);
            nearCells.Add(_maze[cellToRemove.X, cellToRemove.Y + 1]);
            nearCells.Add(_maze[cellToRemove.X, cellToRemove.Y - 1]);

            return nearCells.Where(x => x != null).OfType<T>().ToList();
        }

        private T GetRandomCell<T>(List<T> cells)
        {
            var randIdnex = rand.Next(cells.Count);
            return cells[randIdnex];
        }
    }
}
