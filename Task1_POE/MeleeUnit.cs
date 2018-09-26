using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_POE
{
    [Serializable]
    public class MeleeUnit : Unit
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
            set {alive = value; }
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
            MeleeUnit soldier = new MeleeUnit();
            soldier.name = "Soldier";
            soldier.alive = true;
            soldier.X = rX;
            soldier.Y = rY;
            soldier.Health = 10;
            soldier.Speed = 1;
            soldier.AttackRange = 2;

            // MessageBox.Show(r.ToString());
            switch (team)
            {
                case 1:
                    soldier.team = "Blue";
                    
                    break;

                case 2:
                    soldier.team = "Yellow";
                   
                    break;
            }// team assigning

            soldier.Attack = 4;
            soldier.Symbol = 'S';
            return soldier;
        }

        public override void MoveUnit(int mx, int my)
        {
            
            
                if (mx > 0)
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
           

                
            

            


        }// movement

        public override void Combat(Unit Enemy)
        {          
                if (Enemy.attackRange <3)
            {
                MeleeUnit u2 = (MeleeUnit)Enemy;
                u2.health = u2.health - attack;
            }
                else
            {
                RangedUnit u2 = (RangedUnit)Enemy;
                u2.Health = u2.Health - attack;
            }
        }

        public override int FindUnit(Unit enemy )
        {
            int distance;
            if (enemy.attackRange < 3)
            {
                
                MeleeUnit cEnemy = (MeleeUnit)enemy;
                
                distance = (Math.Abs(x - cEnemy.X) + Math.Abs(y - cEnemy.Y));
            }
            else
            {
               
               RangedUnit cEnemy = (RangedUnit)enemy;
                     distance = (Math.Abs(x - cEnemy.X) + Math.Abs(y - cEnemy.Y));

            }



            //MessageBox.Show(distance.ToString() + enemy.ToString());
            return distance;

        }

        public override bool Death()
        {
           if (health > 0 )
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
            
            MeleeUnit MEnemy = null ;
            RangedUnit rEnemy = null;
            int count = 0;
           // int countm = 0;
            int distance =0;
            int distancer = 0;
           // bool ranged = false;
           
            
                foreach (MeleeUnit u in enemyM)
                {
                if (u != null)
                {
                    if (u.alive && u.team != team)
                    {
                        if (u.x != x && u.y != y)
                        {
                            if (count == 0)
                            {
                                count++;
                                distance = (((Math.Abs(x) - Math.Abs(u.x)) + (Math.Abs(y) - Math.Abs(u.y))));

                                MEnemy = u;
                            }
                            else
                            {
                                if (distance > (((Math.Abs(x) - Math.Abs(u.x)) + (Math.Abs(y) - Math.Abs(u.y)))))
                                {
                                    distance = (((Math.Abs(x) - Math.Abs(u.x)) + (Math.Abs(y) - Math.Abs(u.y))));

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
                    if(u != null)
                {
                    if (u.Alive && u.team != team)
                    {
                        if (u.X != x && u.Y != y)
                        {
                            if (count == 0)
                            {
                                count++;
                                distancer = (((Math.Abs(x) - Math.Abs(u.X)) + (Math.Abs(y) - Math.Abs(u.Y))));

                                rEnemy = u;
                            }
                            else
                            {
                                if (distancer > (((Math.Abs(x) - Math.Abs(u.X)) + (Math.Abs(y) - Math.Abs(u.Y)))))
                                {
                                    distancer = (((Math.Abs(x) - Math.Abs(u.X)) + (Math.Abs(y) - Math.Abs(u.Y))));
                                    rEnemy = u;
                                    // ranged = true;
                                }


                            }
                        }
                    }
                }
                    
                  

                }
                if (distancer< distance)
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
            Info = "Name: "+ name+ "\n";
            Info = Info + "x: " + x + "\n";
            Info = Info + "y: " + y + "\n";
            Info = Info + "Health: " + health + "\n";
            Info = Info + "Speed: " + speed + "\n";
            Info = Info + "Attack: " + attack + "\n";
            Info = Info + "Attack Range: " + AttackRange + "\n";
            Info = Info + "Team" + team + "\n";
            Info = Info + "Symbol: " + symbol + "\n";
            return Info;
        }// done

        public override void Save()
        {
            throw new NotImplementedException();
        }

    }
}
