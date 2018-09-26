using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1_POE
{
    [Serializable]
    public class RangedUnit : Unit
    {
        public string Name
        {
            get { return name; }
            set { name = value; }

        }
        
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }



        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Attack
        {
            get { return attack; }
            set { attack = value; }
        }

        public int AttackRange
        {
            get { return attackRange; }
            set { attackRange = value; }
        }

        public string Team
        {
            get { return team; }
            set { team = value; }
        }

        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public override Unit constuctor(int rX, int rY, int team)
        {
            RangedUnit Archer = new RangedUnit();
            Archer.name = "Archer";
            Archer.alive = true;
            Archer.X = rX;
            Archer.Y = rY;
            Archer.Health = 10;
            Archer.Speed = 1;
            Archer.AttackRange = 4;

            switch (team)
            {
                case 1:
                    Archer.team = "Blue";
                  
                    break;

                case 2:
                    Archer.team = "Yellow";
                   
                    break;

            }// team assigning

            Archer.Attack = 4;
            Archer.Symbol = 'S';

            return Archer;
        }

        public override void MoveUnit(int mx, int my)
        {

            if ((mx + speed < 20) && (mx - speed < 0) && (my + speed < 20) && (my - speed < 0))
            {
                if (mx > 0 && mx + speed < 20)
                {
                    x = x + speed;
                }
                else if (mx < 0)
                {
                    x = x - speed;
                }

                if (my > 0)
                {
                    y = y + speed;
                }
                else if (my < 0)
                {
                    y = y - speed;
                }
            }

        }// movement

        public override void Combat(Unit Enemy)
        {
            if (Enemy.attackRange < 3)
            {
                MeleeUnit u2 = (MeleeUnit)Enemy;
                u2.Health = u2.Health - attack;
            }
            else
            {
                RangedUnit u2 = (RangedUnit)Enemy;
                u2.Health = u2.Health - attack;
            }
        }

        public override int FindUnit(Unit enemy)
        {
            int distance;
            if (enemy.attackRange > 2)
            {

                RangedUnit cEnemy = (RangedUnit)enemy;

                distance = (Math.Abs(x - cEnemy.X) + Math.Abs(y - cEnemy.Y));
            }
            else
            {

                MeleeUnit cEnemy = (MeleeUnit)enemy;
                distance = (Math.Abs(x - cEnemy.X) + Math.Abs(y - cEnemy.Y));

            }

            return distance;

        }

        public override bool Death()
        {
            if (health > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public override Unit ReturnPosition(MeleeUnit[] enemyM, RangedUnit[] enemyR)
        {

            MeleeUnit MEnemy = null;
            RangedUnit rEnemy = null;
            int count = 0;
           
            int distance = 0;
            int distancer = 0;
                
                foreach (MeleeUnit u in enemyM)
                {
                if (u != null)
                {
                    if (u.Alive && u.team != team)
                    {
                        if (u.X != x && u.Y != y)
                        {
                            if (count == 0)
                            {
                                count++;
                                distance = ((Math.Abs(x) - Math.Abs(u.X)) + (Math.Abs(y) - Math.Abs(u.Y)));
                            }
                            else
                            {
                                if (distance > ((Math.Abs(x) - Math.Abs(u.X)) + (Math.Abs(y) - Math.Abs(u.Y))))
                                {
                                    distance = ((Math.Abs(x) - Math.Abs(u.X)) + (Math.Abs(y) - Math.Abs(u.Y)));

                                    MEnemy = u;
                                }

                            }
                        }
                    }
                }
                  
                }
            count = 0;
                foreach (RangedUnit u in enemyR)
                {
                if (u != null)
                {
                    if (u.alive && u.team != team)
                    {
                        if (u.X != x && u.Y != y)
                        {
                            if (count == 0)
                            {
                                count++;
                                distancer = ((Math.Abs(x) - Math.Abs(u.x)) + (Math.Abs(y) - Math.Abs(u.y)));

                                rEnemy = u;
                            }
                            if (distancer > ((Math.Abs(x) - Math.Abs(u.x)) + (Math.Abs(y) - Math.Abs(u.y))))
                            {
                                distancer = ((Math.Abs(x) - Math.Abs(u.x)) + (Math.Abs(y) - Math.Abs(u.y)));
                                rEnemy = u;

                            }


                        }
                    }
                }
                  
                }

            if (distance> distancer)
            {
                return rEnemy;
            }
            else
            {
                return MEnemy;
            }

        }

        public override string ToString()
        {
            string Info;
            Info = "Name: " + name + "\n";
            Info = Info + "x: " + x + "\n";
            Info = Info + "y: " + y + "\n";
            Info = Info + "Health: " + health + "\n";
            Info = Info + "Speed: " + speed + "\n";
            Info = Info + "Attack: " + attack + "\n";
            Info = Info + "Attack Range: " + AttackRange + "\n";
            Info = Info + "Team" + team + "\n";
            Info = Info + "Symbol: " + symbol + "\n";
            return Info;
        }

        public override void Save()
        {
            throw new NotImplementedException();
        }

    }
}

