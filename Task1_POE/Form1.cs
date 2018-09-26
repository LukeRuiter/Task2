using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Task1_POE//Thee0
{
    public partial class Form1 : Form
    {
        Map GameMap;
        bool meleeAlive = true;
        bool rangedAlive = true;
        GameEngine TheGame;
        MyButton[,] buttonarray = new MyButton[20, 20];
        RichTextBox info = new RichTextBox();
        Button pause= new Button();
        Button save = new Button();
        Button LoadGame = new Button();
        Label GText = new Label();
        int GameCount = 0;
        bool timer = true;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            GameMap = new Map();
            GameMap.NewBattlefield();
            TheGame = new GameEngine();
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    MyButton map = new MyButton();
                    Map UnitPos = new Map();

                    Point NewPoint = new Point(100 + (x * 25), 100 + (y * 25));
                    Size NewSize = new Size(20, 20);
                    string say = GameMap.Update(x, y);
                    map.Location = NewPoint;
                    map.Size = NewSize;
                    map.Text = say;
                    map.x = x;
                    map.y = y;

                    foreach (MeleeUnit u in GameMap.MeleeList)
                    {
                        if(u != null)
                        {
                            if (x == u.X && y == u.Y)
                            {

                                switch (u.team)
                                {
                                    case "Blue":
                                        u.team = "Blue";
                                        map.BackColor = System.Drawing.Color.Blue;
                                        
                                        break;

                                    case "Green":
                                        u.team = "Green";
                                        map.BackColor = System.Drawing.Color.Green;
                                        
                                        break;

                                    case "Yellow":
                                        u.team = "Yellow";
                                        map.BackColor = System.Drawing.Color.Yellow;
                                      
                                        break;

                                    case "Red":
                                        u.team = "Red";
                                        map.BackColor = System.Drawing.Color.Red;
                                      
                                        break;

                                }
                            }
                        }
                       
                    }

                    foreach (RangedUnit u in GameMap.RangedList)
                    {
                        if (u != null)
                        {
                            if (x == u.X && y == u.Y)
                            {

                                switch (u.team)
                                {
                                    case "Blue":
                                        u.team = "Blue";
                                        map.BackColor = System.Drawing.Color.Blue;
                                       
                                        break;

                                    case "Green":
                                        u.team = "Green";
                                        map.BackColor = System.Drawing.Color.Green;
                                      
                                        break;

                                    case "Yellow":
                                        u.team = "Yellow";
                                        map.BackColor = System.Drawing.Color.Yellow;
                                      
                                        break;

                                    case "Red":
                                        u.team = "Red";
                                        map.BackColor = System.Drawing.Color.Red;
                                        
                                        break;
                                }
                            }
                        }
                       
                    }
                    this.Controls.Add(map);
                    buttonarray[x, y] = map;

                    map.Click += new EventHandler(button_Click);

                }
            } // Make the buttons
            Point newpoint = new Point(100, 600);
            Size newsize = new Size(400, 200);
            info.Location = newpoint;
            info.Size = newsize;
            this.Controls.Add(info);

            newpoint = new Point(500, 700);
            newsize = new Size(100, 50);

            pause.Location = newpoint;
            pause.Size = newsize;
            pause.Text = "> / ||";
            pause.Click += new EventHandler(Pause_Click);
            this.Controls.Add(pause);

            newpoint = new Point(500, 750);
            newsize = new Size(50, 25);

            save.Location = newpoint;
            save.Size = newsize;
            save.Text = "Save";
            save.Click += new EventHandler(Save_Click);
            this.Controls.Add(save);

            newpoint = new Point(550, 750);
            newsize = new Size(50,25);

            LoadGame.Location = newpoint;
            LoadGame.Size = newsize;
            LoadGame.Text = "Load";
            LoadGame.Click += new EventHandler(Load_Click);

            this.Controls.Add(LoadGame);

            newpoint = new Point(500, 600);
            newsize = new Size(100, 50);

            GText.Location = newpoint;
            GText.Size = newsize;
            GText.Text = GameCount.ToString();

            this.Controls.Add(GText);

        }// placing all buttons and declaring everything

        private void Load_Click(object sender, EventArgs e)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fStream = new FileStream("GameMap.dat", FileMode.Open, FileAccess.Read, FileShare.None);           
            
            try
            {
                GameMap = (Map)bf.Deserialize(fStream);
                MessageBox.Show("Game Loaded");
            }
            catch
            {
                MessageBox.Show("Error occered");
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            
            BinaryFormatter b = new BinaryFormatter();
            FileStream fStream = new FileStream("GameMap.dat", FileMode.Create, FileAccess.Write, FileShare.None);
                       
                try
                {
                    using (fStream)
                    {
                        b.Serialize(fStream, GameMap);
                        MessageBox.Show("Game saved");
                    }
                }
                catch
                {
                    MessageBox.Show("Unable to save Game");
                }
            
        }// saving the game

        private void Pause_Click(object sender, EventArgs e)
        {
            if (timer)
            {
                tmrGame.Enabled = false;
                timer = false;
            }
            else
            {
                tmrGame.Enabled = true;
                timer = true;
            }

        }

        private void updatebuttons()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    buttonarray[x, y].Text = "";
                    buttonarray[x, y].BackColor = System.Drawing.Color.LightGray;
                }
            }

            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    for (int i =0; i <4; i ++)
                    {
                        if (i < 2)
                        {
                            RecourceBuilding b2 = (RecourceBuilding)GameMap.buildingList[i];
                            if (x == b2.X && y == b2.Y)
                            {
                                buttonarray[x, y].Text = "R";
                                switch (b2.Team)
                                {
                                    case "Blue":
                                        b2.Team = "Blue";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Blue;

                                        break;

                                    case "Yellow":
                                        b2.Team = "Yellow";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Yellow;
                                        break;

                                }
                            }
                        }
                        else
                        {
                            Factory b2 = (Factory)GameMap.buildingList[i];
                            if (x == b2.X && y == b2.Y)
                            {
                                buttonarray[x, y].Text = "F";
                                switch (b2.Team)
                                {
                                    case "Blue":
                                        b2.Team = "Blue";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Blue;

                                        break;

                                    case "Yellow":
                                        b2.Team = "Yellow";
                                        buttonarray[x, y].BackColor = System.Drawing.Color.Yellow;
                                        break;

                                }
                            }
                        }
                    }// update buildings

                    foreach (MeleeUnit u in GameMap.MeleeList )
                    {
                        if (u != null)
                        {
                            if (u.Alive)
                            {
                                if (x == u.X && y == u.Y)
                                {
                                    buttonarray[x, y].Text = "S";
                                    switch (u.team)
                                    {
                                        case "Blue":
                                            
                                            buttonarray[x, y].BackColor = System.Drawing.Color.Blue;

                                            break;


                                        case "Yellow":
                                            buttonarray[x, y].BackColor = System.Drawing.Color.Yellow;
                                         
                                            break;

                                    }
                                }
                            }
                        }
                        
                        
                    }// update the map melee

                    foreach (RangedUnit u in GameMap.RangedList)
                    {
                        if (u != null)
                        {
                            if (u.Alive)
                            {
                                if (x == u.X && y == u.Y)
                                {
                                    buttonarray[x, y].Text = "A";
                                    switch (u.team)
                                    {
                                        case "Blue":                                           
                                            buttonarray[x, y].BackColor = System.Drawing.Color.Blue;
                                         
                                            break;

                                        case "Green":                                        
                                            buttonarray[x, y].BackColor = System.Drawing.Color.Green;
                                          
                                            break;

                                        case "Yellow":                                         
                                            buttonarray[x, y].BackColor = System.Drawing.Color.Yellow;
                                           
                                            break;

                                        case "Red":                                           
                                            buttonarray[x, y].BackColor = System.Drawing.Color.Red;
                                           
                                            break;

                                    }
                                }
                            }
                        }
                       
                        
                    }// update the map ranged


                }
            }
        }// displays positions of units and buildings

        public void button_Click(object sender, EventArgs e)
        {
            

            if (((MyButton)sender).Text == "S" || ((MyButton)sender).Text == "A" || ((MyButton)sender).Text == "R"|| ((MyButton)sender).Text == "F")
            {
                for (int i = 0; i < 4; i++)
                {
                    if (i < 2)
                    {
                        RecourceBuilding b2 = (RecourceBuilding)GameMap.buildingList[i];
                        if (((MyButton)sender).x == b2.X && ((MyButton)sender).y == b2.Y)
                        {
                            
                            info.Text = b2.ToString();
                        }
                        
                    }
                    else
                    {
                        Factory b2 = (Factory)GameMap.buildingList[i];
                        if (((MyButton)sender).x == b2.X && ((MyButton)sender).y == b2.Y)
                        {
                            info.Text = b2.ToString();
                        }
                      
                    }
                }


                foreach (MeleeUnit u in GameMap.MeleeList)
                {

                    if (u!= null)
                    {
                        if (((MyButton)sender).x == u.X && ((MyButton)sender).y == u.Y)
                        {
                            MeleeUnit newunit = new MeleeUnit();
                            newunit.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                            info.Text = u.ToString();

                        }
                    }                    
                }

                foreach (RangedUnit u in GameMap.RangedList)
                {

                    if (u != null)
                    {
                        if (((MyButton)sender).x == u.X && ((MyButton)sender).y == u.Y)
                        {
                            RangedUnit newunit = new RangedUnit();
                            newunit.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                            info.Text = u.ToString();

                        }
                    }

                }


            }
            else
            {
                info.Text = ((MyButton)sender).x.ToString() + " " + ((MyButton)sender).y.ToString();
            }

        }// dlisplay unit details

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            int count = 0;

            foreach (MeleeUnit u in GameMap.MeleeList)
            {
                if (u != null)
                {
                    if (u.Alive)
                    {
                        count++;
                    }
                }
               
            }

            if (count == 0)
            {
                meleeAlive = false;
            }

            if (meleeAlive)
            {
                foreach (MeleeUnit u in GameMap.MeleeList)
                {
                    if (u != null)
                    {
                        if (u.Alive)
                        {
                            if (count > 0)
                            {
                                CombatEngine(u);
                            }
                        }
                    }
                                      
                }
            }
     
            count = 0;
           
            foreach (RangedUnit u in GameMap.RangedList)
            {
                if (u != null)
                {
                    if (u.Alive)
                    {
                        count++;
                    }
                }                

            }

            if (count == 0)
            {
                rangedAlive = false;
            }
        
            if (rangedAlive)
            {
                foreach (RangedUnit u in GameMap.RangedList)
                {
                    if (u!= null)
                    {
                        if (u.Alive)
                        {
                            if (count > 0)
                            {
                                CombatEngine(u);
                            }
                        }
                    }
                    

                }
            }
            else
            {
              //  MessageBox.Show("Ranged units dead");
            }

            int unitcount = 0;
            for (int i = 0; i < 15; i++)
            {
                if (GameMap.MeleeList[i]!= null)
                {
                    unitcount++;
                }
                
            }

            RecourceBuilding b1 = (RecourceBuilding)GameMap.buildingList[0];
            b1.generate(b1.AmountTick);
            GameMap.availableBlue = GameMap.availableBlue + b1.AmountTick;

            b1 = (RecourceBuilding)GameMap.buildingList[1];
            b1.generate(b1.AmountTick);
            GameMap.availableYellow = GameMap.availableYellow + b1.AmountTick;

            if (unitcount < 14)
            {
                RecourceBuilding b = (RecourceBuilding)GameMap.buildingList[0];
                if (b.Remaining > 0)
                {     
                    GameMap.availableBlue = GameMap.availableBlue -5;
                    Factory factory = (Factory)GameMap.buildingList[2];
                    GameMap.MeleeList[unitcount]= (MeleeUnit)factory.SpawnUnit();
                }

                 b = (RecourceBuilding)GameMap.buildingList[1];
                if (b.Remaining > 0)
                {
                    GameMap.availableYellow = GameMap.availableYellow - 5;
                    Factory factory = (Factory)GameMap.buildingList[3];
                    GameMap.MeleeList[unitcount+1] = (MeleeUnit)factory.SpawnUnit();
                }
            }
          

            GameCount++;
            GText.Text = GameCount.ToString();
            updatebuttons();
            
        }// activates the game engine
       
        public void CombatEngine(Unit u)
        {
           
                if (u.attackRange < 3)
                {

                     MeleeUnit u2 = (MeleeUnit)u;
                if ((u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList)) != null)
                {
                    int distance = u2.FindUnit(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));
                    if (distance <= u2.attackRange)
                    {
                        u2.Combat(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));

                    } //combat
                    else
                    {
                        if (u2.Health > 2)
                        {
                            if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange < 3)
                            {
                                MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                if (Position.X > u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X < u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }
                            else
                            {
                                RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                if (Position.X > u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X < u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }

                        }// movement
                        else
                        {
                            if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange < 3)
                            {
                                MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                
                                if (Position.X < u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X > u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }
                            else
                            {
                                RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);

                                if (Position.X < u2.X)
                                {

                                    u.MoveUnit(1, 0);
                                }
                                else if (Position.X > u2.X)
                                {

                                    u.MoveUnit(-1, 0);
                                }
                                if (Position.Y < u2.Y)
                                {
                                    u.MoveUnit(0, 1);
                                }
                                else if (Position.Y > u2.Y)
                                {
                                    u.MoveUnit(0, -1);
                                }
                            }

                        }// running away / needs work

                    }

                    if (u.Death())
                    {
                       for (int i =0; i< 15; i++)
                        {
                            if (GameMap.MeleeList[i] != null)
                            {
                                if (GameMap.MeleeList[i]== u)
                                {
                                    for (int k = i; k < 14; k++)
                                    {
                                        GameMap.MeleeList[k] = GameMap.MeleeList[k + 1];
                                    }

                                    GameMap.MeleeList[14] = null;
                                }
                            }
                        }

                    } // handels death in the game
                }// melee

                }
                    
                else
                {
                RangedUnit u2 = (RangedUnit)u;
                int counti = 0;
                    for (int i = 0; i < 5; i++)
                    {
                        if (GameMap.MeleeList[i] != null && u.ReturnPosition(GameMap.MeleeList, GameMap.RangedList) != null)
                        {
                            if (u.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).team != u.team)
                            {
                                counti++;
                            }

                        }
                    }
                    if (counti > 0) // see number of units in array
                    {

                       
                        int distance = u2.FindUnit(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));

                        if (distance <= u2.attackRange)
                        {
                            u2.Combat(u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList));

                        } //combat
                        else
                        {
                            if (u2.Health > 2)
                            {
                                if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange > 3)
                                {
                                    RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                    if (Position.X > u2.X)
                                    {
                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }
                                    if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }
                                else
                                {
                                    MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                    if (Position.X > u2.X)
                                    {
                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }
                                    if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }

                            }// movement
                            else
                            {
                                if (u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList).attackRange > 3)
                                {
                                    RangedUnit Position = (RangedUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                    if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X > u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }


                                    if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }
                                else
                                {
                                    MeleeUnit Position = (MeleeUnit)u2.ReturnPosition(GameMap.MeleeList, GameMap.RangedList);
                                if (u2.X + u2.Speed >= 0 && u2.X + u2.Speed <= 19)
                                {


                                    if (Position.X < u2.X)
                                    {

                                        u.MoveUnit(1, 0);
                                    }
                                    else if (Position.X > u2.X)
                                    {

                                        u.MoveUnit(-1, 0);
                                    }
                                }

                                if (u2.Y + u2.Speed>= 0 && u2.Y + u2.Speed<= 19)
                                {


                                    if (Position.Y < u2.Y)
                                    {
                                        u.MoveUnit(0, 1);
                                    }
                                    else if (Position.Y > u2.Y)
                                    {
                                        u.MoveUnit(0, -1);
                                    }
                                }

                                }

                            }// running away / needs work
                        }

                    }

                    if (u.Death())
                    {

                    for (int i = 0; i < 15; i++)
                    {
                        if (GameMap.RangedList[i] != null)
                        {
                            if (GameMap.RangedList[i] == u)
                            {
                                for (int k = i; k < 14; k++)
                                {
                                    GameMap.RangedList[k] = GameMap.RangedList[k + 1];
                                }

                                GameMap.RangedList[14] = null;
                            }
                        }
                    }


                } // handels death in the game
            }

        }// the working of the game
        

    }

}
