using Lab.LabirinthModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab
{
    public class Maze
    {
        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            isPassed = false;
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var cell = new Wall(x, y);
                    Cells.Add(cell);
                }
            }
        }

        public Cell this[int x, int y] {
            get
            {
                return Cells.SingleOrDefault(cell => cell.X == x && cell.Y == y);
            }

            set
            {
                var oldCell = this[value.X, value.Y];
                if (oldCell != null)
                {
                    Cells.Remove(oldCell);
                }

                Cells.Add(value);
            }
        }
        public Enemy _boss;
        public bool isPassed;
        private int Counter = 0;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Cell> Cells { get; set; } = new List<Cell>();
        private Random randomizer=new Random();
        public void MakeMove(Move direction)
        {
            if (_boss.isDead)
                isPassed = true;
            Cell cellToMove = null;
            var hero = Hero.GetHero;
            switch (direction)
            {
                case Move.Up:
                    cellToMove = this[hero.X, hero.Y - 1];
                    break;
                case Move.Right:
                    cellToMove = this[hero.X +1, hero.Y ];
                    break;
                case Move.Down:
                    cellToMove = this[hero.X, hero.Y + 1];
                    break;
                case Move.Left:
                    cellToMove = this[hero.X - 1, hero.Y];
                    break;
            }    
            if (cellToMove?.CanStep(this) ?? false)
            {
                hero.X = cellToMove.X;
                hero.Y = cellToMove.Y;
            }
            if (++Counter == 2)
            {
                Counter = 0;
                MoveBoss();
            }            
        }
        private void MoveBoss()
        {
            if (!_boss.isDead)
            {
                Cell cellBossToMove = null;
                var options = GetNearCell<Cell>(this[_boss.X, _boss.Y]).Where(x=>!(x is Wall));
                cellBossToMove = options.ElementAt(randomizer.Next(0, options.Count()-1));
                if (cellBossToMove != null)
                {
                    ReplaceWithGround(this[_boss.X, _boss.Y]);
                    _boss = BossSteps(this[cellBossToMove.X, cellBossToMove.Y]) as Enemy;
                }
            }
        }

        private List<T> GetNearCell<T>(Cell cellToRemove) where T : Cell
        {
            var nearCells = new List<Cell>();

            nearCells.Add(this[cellToRemove.X + 1, cellToRemove.Y]);
            nearCells.Add(this[cellToRemove.X - 1, cellToRemove.Y]);
            nearCells.Add(this[cellToRemove.X, cellToRemove.Y + 1]);
            nearCells.Add(this[cellToRemove.X, cellToRemove.Y - 1]);

            return nearCells.Where(x => x != null).OfType<T>().ToList();
        }
        public void ReplaceWithGround(Cell cellToStep)
        {
            this[cellToStep.X, cellToStep.Y] = new Ground(cellToStep.X, cellToStep.Y);
        }
        private Cell BossSteps(Cell cellToStep)
        {
           return this[cellToStep.X, cellToStep.Y] = new Enemy(cellToStep.X, cellToStep.Y, _boss.health);
        }

    }
}
