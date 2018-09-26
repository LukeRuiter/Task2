using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task1_POE
{
    class MyButton : Button
    {
        private int X;

        public int x
        {
            get { return X; }
            set { X = value; }
        }

        private int Y;

        public int y
        {
            get { return Y; }
            set { Y = value; }
        }


    }
}
