using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinthe
{
    class Case
    {
        int x;
        int y;

        public Case(int _x , int _y)
        {
            x = _x;
            y = _y;
        }

        public int getX(){return x;}

        public int getY(){return y;}

    }
}
