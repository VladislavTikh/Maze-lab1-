using Lab.LabirinthModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab
{
    public class Hero : Cell
    {
        public int Attacks { get; set; }
        public int Experience { get; set; }

        private static Hero hero;
        public static Hero GetHero {
            get
            {
                if (hero == null)
                {
                    hero = new Hero();
                }
                return hero;
            }
        }

        private Hero() : base(0, 0, 'X') { Attacks = 1; }

        public override bool CanStep(Maze maze)
        {
            throw new NotImplementedException();
        }
    }
}
