using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_POE
{
    [Serializable]
    class Factory : Building
    {
        private string produce;

        public string Produce
        {
            get { return produce; }
            set { produce = value; }
        }

        private int tickProduce;

        public int TickProduce
        {
            get { return tickProduce; }
            set { tickProduce = value; }
        }

        private int spawnPointX;

        public int SpawnPointX
        {
            get { return spawnPointX; }
            set { spawnPointX = value; }
        }

        private int spawnPointY;

        public int SpawnPointY
        {
            get { return spawnPointY; }
            set { spawnPointY = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public string Team
        {
            get { return team; }
            set { team = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public override void Constructor(int bX, int bY, string bTeam)
        {
            x = bX;
            y = bY;
            team = bTeam;
            health = 50;
            symbol = 'F';
            tickProduce = 1;
            produce = "Soldier";

            if (x >10 && x < 21)
            {
                spawnPointX = x - 1;
            }
            else if(x < 10 && x >= 0)
            {
                spawnPointX = x + 1;
            }
            else if (x== 10)
            {
                spawnPointX = x;
            }

            if (y > 10 && y < 21)
            {
                spawnPointY = y - 1;
            }
            else if (y < 10 && y >= 0)
            {
                spawnPointY = y + 1;
            }
            else if (y == 10)
            {
                spawnPointY = y;
            }
        }

        public override void Destroy()
        {
            symbol = 'X';
        }

        public override string ToString()
        {
            string display;

            display = "Factory \n";
            display = display + "Type: " + produce + "\n";
            display = display + "Health: " + health.ToString() + "\n";
            display = display + "Team: " + team + "\n";
            display = display + "Produced per tick: " + tickProduce.ToString() + "\n";
            display = display + "X position: " + x.ToString() + "\n";
            display = display + "Y position: " + y.ToString() + "\n" +"\n";
            display = display + "Spawn X: " + spawnPointX.ToString()+ "\n";
            display = display + "Spawn Y: " + spawnPointY.ToString() + "\n";
            return display;
        }

        public Unit SpawnUnit()
        {
            MeleeUnit soldier = new MeleeUnit();

            soldier.Alive = true;
            soldier.X = spawnPointX;
            soldier.Y =SpawnPointY;
            soldier.Health = 10;
            soldier.Speed = 1;
            soldier.AttackRange = 2;
            soldier.team = team;
            soldier.Attack = 4;
            soldier.Symbol = 'S';
            return soldier;
            
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }

    }
}
