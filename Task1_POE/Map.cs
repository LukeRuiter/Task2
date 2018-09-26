using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_POE
{

    [Serializable]
    class Map
    {
        public int availableBlue=0;
        public int availableYellow=0;
        public string[,] unitMap = new string[20, 20];// array to hold map information
        public Building[] buildingList = new Building[4];
        public RangedUnit[] RangedList = new RangedUnit[15];
        public MeleeUnit[] MeleeList = new MeleeUnit[15];

        public void NewBattlefield()
        {

            Random r = new Random();
            int melee = 5;
            int ranged = 5;
            int countM = 0;
            int count = 0;
            
            for (int i =0; i < 2; i++)
            {
                if (i == 0 )
                {
                    RecourceBuilding GoldMine = new RecourceBuilding();
                    Factory factory = new Factory();
                    int rX = r.Next(20);
                    int rY = r.Next(20);
                    GoldMine.Constructor(rX,rY,"Blue");
                     rX = r.Next(20);
                     rY = r.Next(20);
                    factory.Constructor(rX, rY, "Blue");

                    buildingList[0] = GoldMine;
                    buildingList[2] = factory;

                }
                else
                {
                    RecourceBuilding GoldMine = new RecourceBuilding();
                    Factory factory = new Factory();
                    int rX = r.Next(20);
                    int rY = r.Next(20);
                    GoldMine.Constructor(rX, rY, "Yellow");
                    rX = r.Next(20);
                    rY = r.Next(20);
                    factory.Constructor(rX, rY, "Yellow");
                    buildingList[1] = GoldMine;
                    buildingList[3] = factory;
                }
            }

            for (int i = 0; i < melee; i++)// melee units 
            {

                int rX = r.Next(20);
                int rY = r.Next(20);
                bool placed = false;

                while (!placed && countM < 5)
                {
                    if (unitMap[rX, rY] == null)
                    {
                        int team = r.Next(1, 3);
                        MeleeUnit mU = new MeleeUnit();
                        mU = (MeleeUnit)mU.constuctor(rX, rY, team);

                        placed = true;
                        unitMap[rX, rY] = "S";

                        MeleeList[countM] = mU;
                        countM++;

                    }
                    else
                    {
                        rX = r.Next(20);
                        rY = r.Next(20);
                    }
                }

            }// melee generating 

            for (int i = 0; i < ranged; i++)// Ranged units
            {

                int rX = r.Next(20);
                int rY = r.Next(20);
                bool placed = false;

                while (!placed && count < 5)
                {
                    if (unitMap[rX, rY] == null && unitMap[rX, rY] != "S")
                    {
                        int team = r.Next(1, 3);
                        RangedUnit mU = new RangedUnit();
                        mU = (RangedUnit)mU.constuctor(rX, rY, team);

                        placed = true;
                        unitMap[rX, rY] = "S";
                        RangedList[count] = mU;
                        count++;
                    }
                    else
                    {
                        rX = r.Next(20);
                        rY = r.Next(20);
                    }

                }//ranged generation
            }// populate the map
        }


        public void MoveUnit()
        {
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    unitMap[x, y] = "";
                }
            }         
            // mover units to new location in 2d array
        }

        public string Update(int X, int Y)
        {
         return unitMap[X,Y];
            // show the new map in the lable
        }

    }    
}
