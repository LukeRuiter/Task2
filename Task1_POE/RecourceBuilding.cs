using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_POE
{
    [Serializable]
    class RecourceBuilding : Building
    {
        private string type;

        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        private int amountTick;

        public int AmountTick
        {
            get { return amountTick; }
            set { amountTick = value; }
        }

        private int remaining;

        public int Remaining
        {
            get { return remaining; }
            set { remaining = value; }
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
            type = "Gold";
            amountTick = 5;
            remaining = 100;
            health = 50;
            symbol = 'G';

         
        }

        public override void Destroy()
        {
            symbol = 'X';
        }

        public override string ToString()
        {
            string display;

            display = "Recource type: " + type+ "\n";
            display = display + "Team: " + team + "\n";
            display = display + "Health: " + health + "\n";
            display = display + "Recources per tick: " + amountTick + "\n";
            display = display + "Recources remaining: " + remaining + "\n";
            display = display + "X position: " + x.ToString() + "\n";
            display = display + "Y position: " + y.ToString() + "\n";


            return display;
            
        }

        public void generate(int use)
        {
           remaining = remaining- use;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }
    }
}
