using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_POE
{
    [Serializable]
    abstract public class Unit 
    {
        protected int distance ;
        protected int x;
        protected int y;
        protected int health;
        protected int currentHealth;
        protected int speed;
        protected string name;
        public int attack;
        public int attackRange;
        public string team;
        protected char symbol;
        protected bool alive;
        //methods
        abstract public Unit constuctor(int rx, int ry, int team);
        abstract public void MoveUnit(int x, int y);
        abstract public void Combat( Unit Enemy);
        abstract public int FindUnit(Unit enemy );
        abstract public Unit ReturnPosition(MeleeUnit[] enemyM, RangedUnit[] enemyR);
        abstract public bool Death();
        abstract public string ToString();
        abstract public void Save();

    }

   
    
}
